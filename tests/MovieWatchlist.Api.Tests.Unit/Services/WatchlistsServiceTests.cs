using Moq;
using MovieWatchlist.Api.Models.Requests;
using MovieWatchlist.Api.Services;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;
using Xunit;

namespace MovieWatchlist.Api.Tests.Unit.Services
{
    public class WatchlistsServiceTests
    {
        private Mock<IWatchlistsRepository> _watchlistsRepositoryMock;
        private WatchlistsService _watchlistsService;

        public WatchlistsServiceTests()
        {
            _watchlistsRepositoryMock = new Mock<IWatchlistsRepository>();
            _watchlistsService = new WatchlistsService(_watchlistsRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateWatchlist_GivenPopulatedRequest_ReturnsWatchlistResponse()
        {
            var watchlistName = "watchlist name";
            var createWatchlistRequest = new CreateWatchlistRequest
            {
                Name = watchlistName,
                MovieIds = new List<string>
                {
                    "movieId"
                }
            };

            var watchlistId = Guid.NewGuid();

            var createdWatchlist = new Watchlist
            {
                Id = watchlistId,
                Name = watchlistName
            };

            _watchlistsRepositoryMock.Setup(m => m.AddWatchlist(It.Is<Watchlist>(w => w.Name.Equals(createWatchlistRequest.Name)))).ReturnsAsync(createdWatchlist);
            _watchlistsRepositoryMock.Setup(m => m.AddWatchlistsMovies(It.Is<IEnumerable<WatchlistsMovies>>(watchlistsMovies => 
                watchlistsMovies.Single().WatchlistId.Equals(watchlistId) && watchlistsMovies.Single().MovieId.Equals("movieId"))));

            var result = await _watchlistsService.CreateWatchlist(createWatchlistRequest);

            Assert.Equal(watchlistId, result.Id);
            Assert.Equal("watchlist name", result.Name);

            _watchlistsRepositoryMock.Verify(m => m.AddWatchlist(It.Is<Watchlist>(w => w.Name.Equals(createWatchlistRequest.Name))), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.AddWatchlistsMovies(It.Is<IEnumerable<WatchlistsMovies>>(watchlistsMovies =>
                watchlistsMovies.Single().WatchlistId.Equals(watchlistId) && watchlistsMovies.Single().MovieId.Equals("movieId"))), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateWatchlist_GivenNullPropertiesInRequest_ReturnsWatchlistResponse()
        {
            var defaultName = "Movies to watch from IMDb Top 250";

            var createWatchlistRequest = new CreateWatchlistRequest
            {
            };

            var watchlistId = Guid.NewGuid();

            var createdWatchlist = new Watchlist
            {
                Id = watchlistId,
                Name = defaultName
            };

            _watchlistsRepositoryMock.Setup(m => m.AddWatchlist(It.Is<Watchlist>(w => w.Name.Equals(createWatchlistRequest.Name)))).ReturnsAsync(createdWatchlist);
            _watchlistsRepositoryMock.Setup(m => m.AddWatchlistsMovies(It.Is<IEnumerable<WatchlistsMovies>>(watchlistsMovies =>
                watchlistsMovies.Any() == false)));

            var result = await _watchlistsService.CreateWatchlist(createWatchlistRequest);

            Assert.Equal(watchlistId, result.Id);
            Assert.Equal(defaultName, result.Name);

            _watchlistsRepositoryMock.Verify(m => m.AddWatchlist(It.Is<Watchlist>(w => w.Name.Equals(createWatchlistRequest.Name))), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.AddWatchlistsMovies(It.Is<IEnumerable<WatchlistsMovies>>(watchlistsMovies =>
                watchlistsMovies.Any() == false)), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetWatchlist_ReturnsWatchlist()
        {
            var watchlistId = Guid.NewGuid();

            var watchlist = new Watchlist
            {
                Id = watchlistId,
                Name = "watchlist name",
                WatchlistsMovies = new List<WatchlistsMovies> { new WatchlistsMovies { Id = 1, WatchlistId = watchlistId } }
            };

            _watchlistsRepositoryMock.Setup(m => m.GetWatchlistById(watchlistId)).ReturnsAsync(watchlist);

            var result = await _watchlistsService.GetWatchlist(watchlistId);

            Assert.Equal(watchlist.Id, result.Id);
            Assert.Equal(watchlist.Name, result.Name);

            _watchlistsRepositoryMock.Verify(m => m.GetWatchlistById(watchlistId), Times.Once);
        }

        [Fact]
        public async Task DeleteWatchlist_ReturnsTask()
        {
            var watchlistId = Guid.NewGuid();
            var watchlistsMovies = new List<WatchlistsMovies>();
            var watchlist = new Watchlist();

            _watchlistsRepositoryMock.Setup(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId)).ReturnsAsync(watchlistsMovies);
            _watchlistsRepositoryMock.Setup(m => m.RemoveWatchlistsMovies(watchlistsMovies));
            _watchlistsRepositoryMock.Setup(m => m.GetWatchlistById(watchlistId)).ReturnsAsync(watchlist);
            _watchlistsRepositoryMock.Setup(m => m.RemoveWatchlist(watchlist));

            await _watchlistsService.DeleteWatchlist(watchlistId);

            _watchlistsRepositoryMock.Verify(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.RemoveWatchlistsMovies(watchlistsMovies), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.GetWatchlistById(watchlistId), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.RemoveWatchlist(watchlist), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddMoviesToWatchlist_ReturnsTask()
        {
            var addMoviesToWatchlistRequest = new AddMoviesToWatchlistRequest
            {
                WatchlistId = Guid.NewGuid(),
                MovieIds = new List<string> { "movieId" }
            };

            _watchlistsRepositoryMock.Setup(m => m.AddWatchlistsMovies(It.Is<IEnumerable<WatchlistsMovies>>(watchlistsMovies =>
                watchlistsMovies.Single().WatchlistId.Equals(addMoviesToWatchlistRequest.WatchlistId) && watchlistsMovies.Single().MovieId.Equals("movieId")))).Returns(Task.CompletedTask);

            await _watchlistsService.AddMoviesToWatchlist(addMoviesToWatchlistRequest);

            _watchlistsRepositoryMock.Verify(m => m.AddWatchlistsMovies(It.Is<IEnumerable<WatchlistsMovies>>(watchlistsMovies =>
                watchlistsMovies.Single().WatchlistId.Equals(addMoviesToWatchlistRequest.WatchlistId) && watchlistsMovies.Single().MovieId.Equals("movieId"))), Times.Once);

            _watchlistsRepositoryMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RemoveMoviesFromWatchlist_ReturnsTask()
        {
            var removeMoviesFromWatchlistRequest = new RemoveMoviesFromWatchlistRequest
            {
                WatchlistId = Guid.NewGuid(),
                MovieIds = new List<string> { "movieId" }
            };

            _watchlistsRepositoryMock.Setup(m => m.RemoveWatchlistsMovies(It.Is<IEnumerable<WatchlistsMovies>>(watchlistsMovies =>
                watchlistsMovies.Single().WatchlistId.Equals(removeMoviesFromWatchlistRequest.WatchlistId) && watchlistsMovies.Single().MovieId.Equals("movieId"))));

            await _watchlistsService.RemoveMoviesFromWatchlist(removeMoviesFromWatchlistRequest);

            _watchlistsRepositoryMock.Verify(m => m.RemoveWatchlistsMovies(It.Is<IEnumerable<WatchlistsMovies>>(watchlistsMovies =>
                watchlistsMovies.Single().WatchlistId.Equals(removeMoviesFromWatchlistRequest.WatchlistId) && watchlistsMovies.Single().MovieId.Equals("movieId"))), Times.Once);

            _watchlistsRepositoryMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task SetMoviesAsWatched_ReturnsTask()
        {
            var setMoviesAsWatchedRequest = new SetMoviesWatchedStatusRequest
            {
                WatchlistId = Guid.NewGuid(),
                MovieIdsWatched = new Dictionary<string, bool> { ["movieId"] = true, ["movieId2"] = false }
            };

            var watchlistsMovies = new List<WatchlistsMovies> { 
                new WatchlistsMovies { WatchlistId = setMoviesAsWatchedRequest.WatchlistId, MovieId = "movieId", Watched = false },
                new WatchlistsMovies { WatchlistId = setMoviesAsWatchedRequest.WatchlistId, MovieId = "movieId2", Watched = true },
                new WatchlistsMovies { WatchlistId = setMoviesAsWatchedRequest.WatchlistId, MovieId = "movieId2", Watched = false }
            };

            _watchlistsRepositoryMock.Setup(m => m.GetWatchlistsMoviesByWatchlistId(setMoviesAsWatchedRequest.WatchlistId)).ReturnsAsync(watchlistsMovies);

            await _watchlistsService.SetMoviesAsWatched(setMoviesAsWatchedRequest);

            Assert.True(watchlistsMovies[0].Watched);
            Assert.False(watchlistsMovies[1].Watched);
            Assert.False(watchlistsMovies[2].Watched);

            _watchlistsRepositoryMock.Verify(m => m.GetWatchlistsMoviesByWatchlistId(setMoviesAsWatchedRequest.WatchlistId), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }
    }
}
