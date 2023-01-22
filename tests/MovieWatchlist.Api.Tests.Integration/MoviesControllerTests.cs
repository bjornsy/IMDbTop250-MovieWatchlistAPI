using MovieWatchlist.Api.Models.Responses;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace MovieWatchlist.Api.Tests.Integration
{
    public class MoviesControllerTests : IClassFixture<MovieWatchlistApiFactory>
    {
        private readonly HttpClient _httpClient;

        public MoviesControllerTests(MovieWatchlistApiFactory movieWatchlistApiFactory)
        {
            _httpClient = movieWatchlistApiFactory.CreateClient();
        }

        [Fact]
        public async Task GetTop250Movies_ReturnsTop250Movies()
        {
            var response = await _httpClient.GetAsync("movies");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var moviesResponse = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<MovieResponse>>();
            Assert.Equal(250, moviesResponse!.Count);
        }
    }
}
