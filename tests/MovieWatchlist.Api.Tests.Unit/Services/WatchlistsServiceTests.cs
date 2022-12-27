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

            _watchlistsRepositoryMock.Setup(m => m.DeleteWatchlist(watchlistId)).Returns(Task.CompletedTask);

            await _watchlistsService.DeleteWatchlist(watchlistId);

            _watchlistsRepositoryMock.Verify(m => m.DeleteWatchlist(watchlistId), Times.Once);
        }

        [Fact]
        public async Task AddMoviesToWatchlist_ReturnsTask()
        {
            var addMoviesToWatchlistRequest = new AddMoviesToWatchlistRequest
            {
                WatchlistId = Guid.NewGuid(),
                MovieIds = new List<string>()
            };

            _watchlistsRepositoryMock.Setup(m => m.AddMoviesToWatchlist(addMoviesToWatchlistRequest.WatchlistId, addMoviesToWatchlistRequest.MovieIds)).Returns(Task.CompletedTask);

            await _watchlistsService.AddMoviesToWatchlist(addMoviesToWatchlistRequest);

            _watchlistsRepositoryMock.Verify(m => m.AddMoviesToWatchlist(addMoviesToWatchlistRequest.WatchlistId, addMoviesToWatchlistRequest.MovieIds), Times.Once);
        }

        [Fact]
        public async Task RemoveMoviesFromWatchlist_ReturnsTask()
        {
            var removeMoviesFromWatchlistRequest = new RemoveMoviesFromWatchlistRequest
            {
                WatchlistId = Guid.NewGuid(),
                MovieIds = new List<string>()
            };

            _watchlistsRepositoryMock.Setup(m => m.RemoveMoviesFromWatchlist(removeMoviesFromWatchlistRequest.WatchlistId, removeMoviesFromWatchlistRequest.MovieIds)).Returns(Task.CompletedTask);

            await _watchlistsService.RemoveMoviesFromWatchlist(removeMoviesFromWatchlistRequest);

            _watchlistsRepositoryMock.Verify(m => m.RemoveMoviesFromWatchlist(removeMoviesFromWatchlistRequest.WatchlistId, removeMoviesFromWatchlistRequest.MovieIds), Times.Once);
        }

        [Fact]
        public async Task SetMoviesAsWatched_ReturnsTask()
        {
            var setMoviesAsWatchedRequest = new SetMoviesAsWatchedRequest
            {
                WatchlistId = Guid.NewGuid(),
                MovieIds = new List<string>()
            };

            _watchlistsRepositoryMock.Setup(m => m.SetMoviesAsWatched(setMoviesAsWatchedRequest.WatchlistId, setMoviesAsWatchedRequest.MovieIds)).Returns(Task.CompletedTask);

            await _watchlistsService.SetMoviesAsWatched(setMoviesAsWatchedRequest);

            _watchlistsRepositoryMock.Verify(m => m.SetMoviesAsWatched(setMoviesAsWatchedRequest.WatchlistId, setMoviesAsWatchedRequest.MovieIds), Times.Once);
        }
    }
}
