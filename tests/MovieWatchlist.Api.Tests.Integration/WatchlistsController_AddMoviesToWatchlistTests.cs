using Microsoft.AspNetCore.Mvc;
using MovieWatchlist.Api.Models.Requests;
using MovieWatchlist.Api.Models.Responses;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace MovieWatchlist.Api.Tests.Integration
{
    public class WatchlistsController_AddMoviesToWatchlistTests : IClassFixture<MovieWatchlistApiFactory>
    {
        private readonly HttpClient _httpClient;

        public WatchlistsController_AddMoviesToWatchlistTests(MovieWatchlistApiFactory movieWatchlistApiFactory)
        {
            _httpClient = movieWatchlistApiFactory.CreateClient();
        }

        [Fact]
        public async Task AddMoviesToWatchlist_WhenWatchlistExists_Returns204()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankAndTheGodfatherWatchlist", MovieIds = new List<string> { "0111161" } };

            var createResponse = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var createdWatchlist = await createResponse.Content.ReadFromJsonAsync<WatchlistResponse>();

            var addMoviesToWatchlistRequest = new AddMoviesToWatchlistRequest { MovieIds = new List<string> { "0068646" } };

            var response = await _httpClient.PostAsJsonAsync($"watchlists/{createdWatchlist!.Id}/addMovies", addMoviesToWatchlistRequest);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task AddMoviesToWatchlist_WhenWatchlistDoesNotExist_Returns404()
        {
            var addMoviesToWatchlistRequest = new AddMoviesToWatchlistRequest { MovieIds = new List<string> { "0068646" } };

            var response = await _httpClient.PostAsJsonAsync($"watchlists/{Guid.NewGuid()}/addMovies", addMoviesToWatchlistRequest);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AddMoviesToWatchlist_WhenRequestModelInvalid_Returns400()
        {
            var addMoviesToWatchlistRequest = new AddMoviesToWatchlistRequest { MovieIds = new List<string> { } };

            var response = await _httpClient.PostAsJsonAsync($"watchlists/{Guid.NewGuid()}/addMovies", addMoviesToWatchlistRequest);

            var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("One or more validation errors occurred.", error!.Title);
            Assert.Equal("The field MovieIds must be a string or array type with a minimum length of '1'.", error!.Errors["MovieIds"].Single());
        }

        [Fact]
        public async Task AddMoviesToWatchlist_GivenUnknownMovieId_Returns422()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankWatchlist", MovieIds = new List<string> { "0111161" } };

            var createResponse = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var createdWatchlist = await createResponse.Content.ReadFromJsonAsync<WatchlistResponse>();

            var addMoviesToWatchlistRequest = new AddMoviesToWatchlistRequest { MovieIds = new List<string> { Guid.NewGuid().ToString() } };

            var response = await _httpClient.PostAsJsonAsync($"watchlists/{createdWatchlist!.Id}/addMovies", addMoviesToWatchlistRequest);
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
            Assert.Equal("Movie ID(s) supplied does not exist in the top 250.", problemDetails!.Detail);
            Assert.Equal("Value in request is invalid.", problemDetails.Title);
            Assert.Equal(422, problemDetails.Status);
            Assert.Equal("https://datatracker.ietf.org/doc/html/rfc4918#section-11.2", problemDetails.Type);
        }
    }
}
