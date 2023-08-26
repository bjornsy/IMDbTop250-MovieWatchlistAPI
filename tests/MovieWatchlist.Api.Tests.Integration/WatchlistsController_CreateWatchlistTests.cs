using Microsoft.AspNetCore.Mvc;
using MovieWatchlist.Api.Models.Requests;
using MovieWatchlist.Api.Models.Responses;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace MovieWatchlist.Api.Tests.Integration
{
    public class WatchlistsController_CreateWatchlistTests : IClassFixture<MovieWatchlistApiFactory>
    {
        private readonly HttpClient _httpClient;

        public WatchlistsController_CreateWatchlistTests(MovieWatchlistApiFactory movieWatchlistApiFactory)
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
    }
}
