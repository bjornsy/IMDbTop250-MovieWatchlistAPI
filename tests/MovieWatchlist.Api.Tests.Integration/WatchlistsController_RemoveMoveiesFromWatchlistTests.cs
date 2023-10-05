using Microsoft.AspNetCore.Mvc;
using MovieWatchlist.Contracts.Requests;
using MovieWatchlist.Contracts.Responses;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace MovieWatchlist.Api.Tests.Integration
{
    public class WatchlistsController_RemoveMoveiesFromWatchlistTests : IClassFixture<MovieWatchlistApiFactory>
    {
        private readonly HttpClient _httpClient;

        public WatchlistsController_RemoveMoveiesFromWatchlistTests(MovieWatchlistApiFactory movieWatchlistApiFactory)
        {
            _httpClient = movieWatchlistApiFactory.CreateClient();
        }

        [Fact]
        public async Task RemoveMoviesFromWatchlist_WhenWatchlistExists_Returns204()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankWatchlist", MovieIds = new List<string> { "0111161" } };

            var createResponse = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var createdWatchlist = await createResponse.Content.ReadFromJsonAsync<WatchlistResponse>();

            var removeMoviesToWatchlistRequest = new RemoveMoviesFromWatchlistRequest { MovieIds = new List<string> { "0111161" } };

            var response = await _httpClient.PostAsJsonAsync($"watchlists/{createdWatchlist!.Id}/removeMovies", removeMoviesToWatchlistRequest);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var updatedWatchlist = await (await _httpClient.GetAsync($"watchlists/{createdWatchlist.Id!}")).Content.ReadFromJsonAsync<WatchlistResponse>();
            Assert.True(updatedWatchlist!.Movies.Count.Equals(0));
        }

        [Fact]
        public async Task RemoveMoviesFromWatchlist_WhenWatchlistDoesNotExist_Returns404()
        {
            var removeMoviesToWatchlistRequest = new RemoveMoviesFromWatchlistRequest { MovieIds = new List<string> { "0111161" } };

            var response = await _httpClient.PostAsJsonAsync($"watchlists/{Guid.NewGuid()}/removeMovies", removeMoviesToWatchlistRequest);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task RemoveMoviesFromWatchlist_WhenRequestInvalid_Returns400()
        {
            var removeMoviesToWatchlistRequest = new RemoveMoviesFromWatchlistRequest { MovieIds = new List<string> { } };

            var response = await _httpClient.PostAsJsonAsync($"watchlists/{Guid.NewGuid()}/removeMovies", removeMoviesToWatchlistRequest);

            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("One or more validation errors occurred.", error!.Title);
            Assert.Equal("The field MovieIds must be a string or array type with a minimum length of '1'.", error!.Errors["MovieIds"].Single());
        }

        [Fact]
        public async Task RemoveMoviesFromWatchlist_GivenUnknownMovieId_Returns422()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankWatchlist", MovieIds = new List<string> { "0111161" } };

            var createResponse = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var createdWatchlist = await createResponse.Content.ReadFromJsonAsync<WatchlistResponse>();

            var invalidMovieId1 = Guid.NewGuid().ToString();
            var invalidMovieId2 = Guid.NewGuid().ToString();
            var removeMoviesFromWatchlistRequest = new RemoveMoviesFromWatchlistRequest { MovieIds = new List<string> { invalidMovieId1, invalidMovieId2 } };

            var response = await _httpClient.PostAsJsonAsync($"watchlists/{createdWatchlist!.Id}/removeMovies", removeMoviesFromWatchlistRequest);
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
            Assert.Equal($"The following movie Ids in the request are invalid: {invalidMovieId1},{invalidMovieId2}", problemDetails!.Detail);
            Assert.Equal("Value in request is invalid.", problemDetails.Title);
            Assert.Equal(422, problemDetails.Status);
            Assert.Equal("https://datatracker.ietf.org/doc/html/rfc4918#section-11.2", problemDetails.Type);
        }
    }
}
