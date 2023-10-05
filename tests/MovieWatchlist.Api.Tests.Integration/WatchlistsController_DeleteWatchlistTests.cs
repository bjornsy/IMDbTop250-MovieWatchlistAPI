using Microsoft.AspNetCore.Mvc;
using MovieWatchlist.Contracts.Requests;
using MovieWatchlist.Contracts.Responses;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace MovieWatchlist.Api.Tests.Integration
{
    public class WatchlistsController_DeleteWatchlistTests : IClassFixture<MovieWatchlistApiFactory>
    {
        private readonly HttpClient _httpClient;

        public WatchlistsController_DeleteWatchlistTests(MovieWatchlistApiFactory movieWatchlistApiFactory)
        {
            _httpClient = movieWatchlistApiFactory.CreateClient();
        }

        [Fact]
        public async Task DeleteWatchlist_WhenWatchlistExists_Returns204()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankWatchlist", MovieIds = new List<string> { "0111161" } };

            var createResponse = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var createdWatchlist = await createResponse.Content.ReadFromJsonAsync<WatchlistResponse>();

            var response = await _httpClient.DeleteAsync($"watchlists/{createdWatchlist!.Id}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            //Check actually deleted
            var getWatchlistResponse = await _httpClient.GetAsync($"watchlists/{createdWatchlist!.Id}");
            Assert.Equal(HttpStatusCode.NotFound, getWatchlistResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteWatchlist_WhenWatchlistDoesNotExist_Returns404()
        {
            var watchlistId = Guid.NewGuid();
            var response = await _httpClient.DeleteAsync($"watchlists/{watchlistId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            //Check actually deleted
            var getWatchlistResponse = await _httpClient.GetAsync($"watchlists/{watchlistId}");
            Assert.Equal(HttpStatusCode.NotFound, getWatchlistResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteWatchlist_WhenRequestIdNotGuid_Returns400BadRequest()
        {
            var response = await _httpClient.DeleteAsync($"watchlists/test");

            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("One or more validation errors occurred.", error!.Title);
            Assert.Equal("The value 'test' is not valid.", error!.Errors["watchlistId"].Single());
        }
    }
}
