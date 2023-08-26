using Microsoft.AspNetCore.Mvc;
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
