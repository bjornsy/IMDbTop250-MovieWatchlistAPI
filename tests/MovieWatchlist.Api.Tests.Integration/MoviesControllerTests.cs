using MovieWatchlist.Contracts.Responses;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace MovieWatchlist.Api.Tests.Integration
{
    public class MoviesControllerTests : IClassFixture<MovieWatchlistApiFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly MovieWatchlistApiFactory _movieWatchlistApiFactory;

        public MoviesControllerTests(MovieWatchlistApiFactory movieWatchlistApiFactory)
        {
            _httpClient = movieWatchlistApiFactory.CreateClient();
            _movieWatchlistApiFactory = movieWatchlistApiFactory;
        }

        [Fact]
        public async Task GetTop250Movies_ReturnsTop250Movies()
        {
            var guid = Guid.NewGuid();
            _movieWatchlistApiFactory.SetTop250Response(HttpStatusCode.OK, guid);

            var response = await _httpClient.GetAsync("movies/top250");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var moviesResponse = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<MovieResponse>>();
            Assert.Equal(250, moviesResponse!.Count);

            AssertSingleWireMockLogEntry(guid);
        }

        [Fact]
        public async Task GetMovies_GivenValidMovieIds_ReturnsMovies()
        {
            var shawshankId = "0111161";
            var godfatherId = "0068646";
            var response = await _httpClient.GetAsync($"movies?movieIds={shawshankId}&movieIds={godfatherId}");
            var movies = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<MovieResponse>>();

            Assert.Equal(2, movies!.Count);
            var shawshankMovie = movies!.Single(m => m.Id.Equals(shawshankId));
            var godfatherMovie = movies!.Single(m => m.Id.Equals(godfatherId));
            Assert.Equal("The Shawshank Redemption (1994)", shawshankMovie.Title);
            Assert.NotEqual(decimal.Zero, shawshankMovie.Rating);
            Assert.NotEqual(0, shawshankMovie.Ranking);
            Assert.Equal("The Godfather (1972)", godfatherMovie.Title);
            Assert.NotEqual(decimal.Zero, godfatherMovie.Rating);
            Assert.NotEqual(0, godfatherMovie.Ranking);
        }

        [Fact]
        public async Task GetMovies_GivenNoIds_Returns400BadRequest()
        {
            var response = await _httpClient.GetAsync("movies");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetMovies_WhenMovieIdDoesNotExist_ReturnsEmpty()
        {
            var response = await _httpClient.GetAsync($"movies?movieIds=INVALID");

            var movies = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<MovieResponse>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(movies!);
        }

        private void AssertSingleWireMockLogEntry(Guid guid)
        {
            var log = _movieWatchlistApiFactory.GetWireMockLogEntries(guid).Single();
            Assert.EndsWith($"/charts/?{DateTime.Now.ToString("yyyy/MM/dd")}", log.RequestMessage.Url);
        }
    }
}
