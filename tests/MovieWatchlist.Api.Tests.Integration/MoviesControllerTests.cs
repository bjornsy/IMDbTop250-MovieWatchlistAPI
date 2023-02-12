using MovieWatchlist.Api.Models.Responses;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace MovieWatchlist.Api.Tests.Integration
{
    public class MoviesControllerTests : IClassFixture<MovieWatchlistApiFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly MovieWatchlistApiFactory _movieWatchlistApiFactory;

        public MoviesControllerTests(MovieWatchlistApiFactory movieWatchlistApiFactory)
        {
            _httpClient = movieWatchlistApiFactory.CreateClient();
            _movieWatchlistApiFactory = movieWatchlistApiFactory;
        }

        [Fact]
        public async Task GetTop250Movies_ReturnsTop250Movies()
        {
            var guid = Guid.NewGuid();
            _movieWatchlistApiFactory.SetTop250Response(HttpStatusCode.OK, guid);

            var response = await _httpClient.GetAsync("movies");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var moviesResponse = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<MovieResponse>>();
            Assert.Equal(250, moviesResponse!.Count);

            AssertSingleWireMockLogEntry(guid);
        }

        private void AssertSingleWireMockLogEntry(Guid guid)
        {
            var log = _movieWatchlistApiFactory.GetWireMockLogEntries(guid).Single();
            Assert.EndsWith($"/charts/?{DateTime.Now.ToString("yyyy/MM/dd")}", log.RequestMessage.Url);
        }
    }
}
