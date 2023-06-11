﻿using MovieWatchlist.Api.Models.Requests;
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
        public async Task DeleteWatchlist_Returns204()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankWatchlist", MovieIds = new List<string> { "0111161" } };

            var createResponse = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var createdWatchlist = await createResponse.Content.ReadFromJsonAsync<WatchlistResponse>();

            var response = await _httpClient.DeleteAsync($"watchlists/{createdWatchlist!.Id}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task AddMoviesToWatchlist_Returns204()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankAndTheGodfatherWatchlist", MovieIds = new List<string> { "0111161" } };

            var createResponse = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var createdWatchlist = await createResponse.Content.ReadFromJsonAsync<WatchlistResponse>();

            var addMoviesToWatchlistRequest = new AddMoviesToWatchlistRequest { WatchlistId = createdWatchlist!.Id, MovieIds = new List<string> { "0068646" } };

            var response = await _httpClient.PostAsJsonAsync("watchlists/addMovies", addMoviesToWatchlistRequest);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task RemoveMoviesFromWatchlist_Returns204()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankWatchlist", MovieIds = new List<string> { "0111161" } };

            var createResponse = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var createdWatchlist = await createResponse.Content.ReadFromJsonAsync<WatchlistResponse>();

            var addMoviesToWatchlistRequest = new RemoveMoviesFromWatchlistRequest { WatchlistId = createdWatchlist!.Id, MovieIds = new List<string> { "0111161" } };

            var response = await _httpClient.PostAsJsonAsync("watchlists/removeMovies", addMoviesToWatchlistRequest);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task SetMoviesStatusWatched_Returns204()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankWatchlist", MovieIds = new List<string> { "0111161" } };

            var createResponse = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var createdWatchlist = await createResponse.Content.ReadFromJsonAsync<WatchlistResponse>();

            var setMoviesWatchedStatusRequest = new SetMoviesWatchedStatusRequest { WatchlistId = createdWatchlist!.Id,  MovieIdsWatched = new Dictionary<string, bool> { ["0111161"] = true } };

            var jsonRequest = JsonSerializer.Serialize(setMoviesWatchedStatusRequest);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync("watchlists/setMoviesWatchedStatus", content);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}