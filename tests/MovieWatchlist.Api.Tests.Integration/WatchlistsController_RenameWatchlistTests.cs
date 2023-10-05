using MovieWatchlist.Contracts.Requests;
using MovieWatchlist.Contracts.Responses;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Xunit;

namespace MovieWatchlist.Api.Tests.Integration
{
    public class WatchlistsController_RenameWatchlistTests : IClassFixture<MovieWatchlistApiFactory>
    {
        private readonly HttpClient _httpClient;

        public WatchlistsController_RenameWatchlistTests(MovieWatchlistApiFactory movieWatchlistApiFactory)
        {
            _httpClient = movieWatchlistApiFactory.CreateClient();
        }

        [Fact]
        public async Task Rename_WhenWatchlistExists_Returns204()
        {
            var createWatchlistRequest = new CreateWatchlistRequest { Name = "ShawshankWatchlist", MovieIds = new List<string> { "0111161" } };

            var createResponse = await _httpClient.PostAsJsonAsync("watchlists", createWatchlistRequest);
            var createdWatchlist = await createResponse.Content.ReadFromJsonAsync<WatchlistResponse>();

            var renameWatchlistRequest = new RenameWatchlistRequest { Name = "NewName" };

            var jsonRequest = JsonSerializer.Serialize(renameWatchlistRequest);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync($"watchlists/{createdWatchlist!.Id}/rename", content);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            
            //Check actually renamed
            var getWatchlistResponse = await _httpClient.GetAsync($"watchlists/{createdWatchlist!.Id}");
            Assert.Equal("NewName", (await getWatchlistResponse.Content.ReadFromJsonAsync<WatchlistResponse>())!.Name);
        }

        [Fact]
        public async Task Rename_WhenWatchlistDoesNotExist_Returns404()
        {
            var renameWatchlistRequest = new RenameWatchlistRequest { Name = "NewName" };

            var jsonRequest = JsonSerializer.Serialize(renameWatchlistRequest);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
            var response = await _httpClient.PatchAsync($"watchlists/{Guid.NewGuid()}/rename", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
