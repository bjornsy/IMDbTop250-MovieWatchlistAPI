using Microsoft.AspNetCore.Mvc;
using MovieWatchlist.Contracts.Requests;
using MovieWatchlist.Contracts.Responses;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Xunit;

namespace MovieWatchlist.Api.Tests.Integration
{
    public class WatchlistsController_SetMoviesStatusWatchedTests : IClassFixture<MovieWatchlistApiFactory>
    {
        private readonly HttpClient _httpClient;

        public WatchlistsController_SetMoviesStatusWatchedTests(MovieWatchlistApiFactory movieWatchlistApiFactory)
        {
            _httpClient = movieWatchlistApiFactory.CreateClient();
        }

        [Fact]
        public async Task SetMoviesStatusWatched_WhenWatchlistMovieExists_Returns204()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankWatchlist", MovieIds = new List<string> { "0111161" } };

            var createResponse = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var createdWatchlist = await createResponse.Content.ReadFromJsonAsync<WatchlistResponse>();

            var setMoviesWatchedStatusRequest = new SetMoviesWatchedStatusRequest { MovieIdsWatched = new Dictionary<string, bool> { ["0111161"] = true } };

            var jsonRequest = JsonSerializer.Serialize(setMoviesWatchedStatusRequest);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync($"watchlists/{createdWatchlist!.Id}/setMoviesWatchedStatus", content);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task SetMoviesStatusWatched_WhenWatchlistDoesNotExist_Returns404()
        {
            var setMoviesWatchedStatusRequest = new SetMoviesWatchedStatusRequest { MovieIdsWatched = new Dictionary<string, bool> { ["0111161"] = true } };

            var jsonRequest = JsonSerializer.Serialize(setMoviesWatchedStatusRequest);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync($"watchlists/{Guid.NewGuid()}/setMoviesWatchedStatus", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task SetMoviesStatusWatched_WhenRequestInvalid_Returns400()
        {
            var setMoviesWatchedStatusRequest = new SetMoviesWatchedStatusRequest { MovieIdsWatched = new Dictionary<string, bool> { } };

            var jsonRequest = JsonSerializer.Serialize(setMoviesWatchedStatusRequest);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync($"watchlists/{Guid.NewGuid()}/setMoviesWatchedStatus", content);

            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("One or more validation errors occurred.", error!.Title);
            Assert.Equal("The field MovieIdsWatched must be a string or array type with a minimum length of '1'.", error!.Errors["MovieIdsWatched"].Single());
        }

        [Fact]
        public async Task SetMoviesStatusWatched_WhenWatchlistDoesNotContainMatchingMovie_Returns422()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankWatchlist", MovieIds = new List<string> { "0111161" } };

            var createResponse = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var createdWatchlist = await createResponse.Content.ReadFromJsonAsync<WatchlistResponse>();

            var invalidMovieId1 = Guid.NewGuid().ToString();
            var invalidMovieId2 = Guid.NewGuid().ToString();
            var setMoviesWatchedStatusRequest = new SetMoviesWatchedStatusRequest { MovieIdsWatched = new Dictionary<string, bool> { [invalidMovieId1] = true, [invalidMovieId2] = false } };

            var jsonRequest = JsonSerializer.Serialize(setMoviesWatchedStatusRequest);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync($"watchlists/{createdWatchlist!.Id}/setMoviesWatchedStatus", content);

            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
            Assert.Equal($"The following movie Ids in the request are invalid: {invalidMovieId1},{invalidMovieId2}", problemDetails!.Detail);
            Assert.Equal("Value in request is invalid.", problemDetails.Title);
            Assert.Equal(422, problemDetails.Status);
            Assert.Equal("https://datatracker.ietf.org/doc/html/rfc4918#section-11.2", problemDetails.Type);
        }
    }
}
