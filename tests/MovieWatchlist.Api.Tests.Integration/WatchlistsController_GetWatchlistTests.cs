using Microsoft.AspNetCore.Mvc;
using MovieWatchlist.Contracts.Requests;
using MovieWatchlist.Contracts.Responses;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace MovieWatchlist.Api.Tests.Integration
{
    public class WatchlistsController_GetWatchlistTests : IClassFixture<MovieWatchlistApiFactory>
    {
        private readonly HttpClient _httpClient;

        public WatchlistsController_GetWatchlistTests(MovieWatchlistApiFactory movieWatchlistApiFactory)
        {
            _httpClient = movieWatchlistApiFactory.CreateClient();
        }

        [Fact]
        public async Task GetWatchlist_Returns200()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankWatchlist", MovieIds = new List<string> { "0111161" } };

            var createResponse = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var createdWatchlist = await createResponse.Content.ReadFromJsonAsync<WatchlistResponse>();

            var response = await _httpClient.GetAsync($"watchlists/{createdWatchlist!.Id}");

            var watchlist = await response.Content.ReadFromJsonAsync<WatchlistResponse>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("ShawshankWatchlist", watchlist!.Name);
            Assert.NotEqual(Guid.Empty, watchlist.Id);
            var movieInResponse = createdWatchlist.Movies.Single();
            Assert.Equal("0111161", movieInResponse.Movie.Id);
            Assert.Equal("The Shawshank Redemption (1994)", movieInResponse.Movie.Title);
            Assert.NotNull(movieInResponse.Movie.Ranking);
            Assert.NotEqual(0, movieInResponse.Movie.Rating);
            Assert.False(movieInResponse.Watched);
        }

        [Fact]
        public async Task GetWatchlist_WhenWatchlistDoesNotExist_Returns404NotFound()
        {
            var response = await _httpClient.GetAsync($"watchlists/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetWatchlist_WhenRequestIdNotGuid_Returns400BadRequest()
        {
            var response = await _httpClient.GetAsync($"watchlists/test");

            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("One or more validation errors occurred.", error!.Title);
            Assert.Equal("The value 'test' is not valid.", error!.Errors["watchlistId"].Single());
        }
    }
}
