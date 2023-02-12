using MovieWatchlist.Api.Models.Responses;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace MovieWatchlist.Api.Tests.Integration
{
    public class MoviesControllerErrorTests : IClassFixture<MovieWatchlistApiFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly MovieWatchlistApiFactory _movieWatchlistApiFactory;

        public MoviesControllerErrorTests(MovieWatchlistApiFactory movieWatchlistApiFactory)
        {
            _httpClient = movieWatchlistApiFactory.CreateClient();
            _movieWatchlistApiFactory = movieWatchlistApiFactory;
        }

        [Fact]
        public async Task GetTop250Movies_WhenWebsiteReturns500_ReturnsTop250FromSeededDatabase()
        {
            var guid = Guid.NewGuid();
            _movieWatchlistApiFactory.SetTop250Response(HttpStatusCode.InternalServerError, guid);

            var response = await _httpClient.GetAsync("movies");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var moviesResponse = (await response.Content.ReadFromJsonAsync<IReadOnlyCollection<MovieResponse>>()).ToList();
            Assert.Equal(250, moviesResponse!.Count);

            var seededMovies = new SeedMoviesLoader().GetSeededTop250Movies().OrderBy(m => m.Ranking).ToList();
            for (int i = 0; i < seededMovies.Count; i++)
            {
                var movieFromSeed = seededMovies[i];
                var movieFromResponse = moviesResponse[i];

                Assert.Equal(movieFromSeed.Id, movieFromResponse.Id);
                Assert.Equal(movieFromSeed.Rating, movieFromResponse.Rating);
            }

            var logs = _movieWatchlistApiFactory.GetWireMockLogEntries(guid);
            logs.ToList().ForEach(l => Assert.EndsWith($"/charts/?{DateTime.Now:yyyy/MM/dd}", l.RequestMessage.Url));
            Assert.Equal(5, logs.Count());
        }
    }
}
