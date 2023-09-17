using MovieWatchlist.Api.Models.Responses;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace MovieWatchlist.Api.Tests.Integration
{
    public class MoviesControllerCacheTests : IClassFixture<MovieWatchlistApiFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly MovieWatchlistApiFactory _movieWatchlistApiFactory;

        public MoviesControllerCacheTests(MovieWatchlistApiFactory movieWatchlistApiFactory)
        {
            _httpClient = movieWatchlistApiFactory.CreateClient();
            _movieWatchlistApiFactory = movieWatchlistApiFactory;
        }

        //Called twice, uses cache, not website or db
        [Fact]
        public async Task GetTop250Movies_GivenCalledTwice_ReturnsTop250FromCache()
        {
            var guid = Guid.NewGuid();
            _movieWatchlistApiFactory.SetTop250Response(HttpStatusCode.OK, guid);

            var response1 = await _httpClient.GetAsync("movies/top250");
            Assert.Equal(HttpStatusCode.OK, response1.StatusCode);
            var moviesResponse1 = (await response1.Content.ReadFromJsonAsync<IReadOnlyCollection<MovieResponse>>()).ToList();
            Assert.Equal(250, moviesResponse1!.Count);

            var response2 = await _httpClient.GetAsync("movies/top250");
            Assert.Equal(HttpStatusCode.OK, response2.StatusCode);
            var moviesResponse2 = (await response2.Content.ReadFromJsonAsync<IReadOnlyCollection<MovieResponse>>()).ToList();
            Assert.Equal(250, moviesResponse2!.Count);

            for (var i = 0; i < moviesResponse1.Count; i++)
            {
                var movieFromFirstResponse = moviesResponse1[i];
                var movieFromSecondResponse = moviesResponse2[i];

                Assert.Equal(movieFromFirstResponse.Id, movieFromSecondResponse.Id);
                Assert.Equal(movieFromFirstResponse.Rating, movieFromSecondResponse.Rating);
            }

            AssertSingleWireMockLogEntry(guid);
        }

        private void AssertSingleWireMockLogEntry(Guid guid)
        {
            var log = _movieWatchlistApiFactory.GetWireMockLogEntries(guid).Single();
            Assert.EndsWith($"/charts/?{DateTime.Now.ToString("yyyy/MM/dd")}", log.RequestMessage.Url);
        }
    }
}
