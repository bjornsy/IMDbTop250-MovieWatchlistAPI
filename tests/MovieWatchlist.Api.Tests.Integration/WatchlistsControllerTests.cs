using Microsoft.AspNetCore.Mvc;
using MovieWatchlist.Api.Models.Requests;
using MovieWatchlist.Api.Models.Responses;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Xunit;

namespace MovieWatchlist.Api.Tests.Integration
{
    public class WatchlistsControllerTests : IClassFixture<MovieWatchlistApiFactory>
    {
        private readonly HttpClient _httpClient;

        public WatchlistsControllerTests(MovieWatchlistApiFactory movieWatchlistApiFactory)
        {
            _httpClient = movieWatchlistApiFactory.CreateClient();
        }

        [Fact]
        public async Task CreateWatchlist_Returns201()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankWatchlist", MovieIds = new List<string> { "0111161" } };

            var response = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var createdWatchlist = await response.Content.ReadFromJsonAsync<WatchlistResponse>();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("ShawshankWatchlist", createdWatchlist!.Name);
            Assert.NotEqual(Guid.Empty, createdWatchlist.Id);
        }

        [Fact]
        public async Task CreateWatchlist_GivenUnknownMovieId_Returns422()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankWatchlist", MovieIds = new List<string> { Guid.NewGuid().ToString() } };

            var response = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
            Assert.Equal("Movie ID(s) supplied does not exist in the top 250.", problemDetails!.Detail);
            Assert.Equal("Value in request is invalid.", problemDetails.Title);
            Assert.Equal(422, problemDetails.Status);
            Assert.Equal("https://datatracker.ietf.org/doc/html/rfc4918#section-11.2", problemDetails.Type);
        }

        [Fact]
        public async Task GetWatchlist_ReturnsWatchlist()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankWatchlist", MovieIds = new List<string> { "0111161" } };
            var createResponse = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var createdWatchlist = await createResponse.Content.ReadFromJsonAsync<WatchlistResponse>();

            var response = await _httpClient.GetAsync($"watchlists/{createdWatchlist!.Id}");
            var watchlist = await response.Content.ReadFromJsonAsync<WatchlistResponse>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(createdWatchlist.Name, watchlist!.Name);
            Assert.Equal(createdWatchlist.Id, watchlist!.Id);
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

        [Fact]
        public async Task RemoveMoviesFromWatchlist_WhenWatchlistExists_Returns204()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankWatchlist", MovieIds = new List<string> { "0111161" } };

            var createResponse = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var createdWatchlist = await createResponse.Content.ReadFromJsonAsync<WatchlistResponse>();

            var removeMoviesToWatchlistRequest = new RemoveMoviesFromWatchlistRequest { MovieIds = new List<string> { "0111161" } };

            var response = await _httpClient.PostAsJsonAsync($"watchlists/{createdWatchlist!.Id}/removeMovies", removeMoviesToWatchlistRequest);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            var moviesInWatchlist = await (await _httpClient.GetAsync($"movies/byWatchlistId/{createdWatchlist.Id!}")).Content.ReadFromJsonAsync<IReadOnlyCollection<MovieInWatchlistResponse>>();
            Assert.True(moviesInWatchlist!.Count.Equals(0));
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
