using Moq;
using MovieWatchlist.ApplicationCore.Exceptions;
using MovieWatchlist.Contracts.Requests;
using MovieWatchlist.ApplicationCore.Services;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;
using Xunit;

namespace MovieWatchlist.ApplicationCore.Tests.Unit.Services
{
    public class WatchlistsServiceTests
    {
        private Mock<IWatchlistsRepository> _watchlistsRepositoryMock;
        private Mock<IMoviesRepository> _moviesRepositoryMock;
        private WatchlistsService _watchlistsService;

        public WatchlistsServiceTests()
        {
            _watchlistsRepositoryMock = new Mock<IWatchlistsRepository>();
            _moviesRepositoryMock = new Mock<IMoviesRepository>();
            _watchlistsService = new WatchlistsService(_watchlistsRepositoryMock.Object, _moviesRepositoryMock.Object);
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

            var moviesRepositoryResult = new List<Movie> { new Movie { Id = "movieId" } };

            _watchlistsRepositoryMock.Setup(m => m.AddWatchlist(It.Is<Watchlist>(w => w.Name.Equals(createWatchlistRequest.Name)))).ReturnsAsync(createdWatchlist);
            _watchlistsRepositoryMock.Setup(m => m.AddWatchlistsMovies(It.Is<IEnumerable<WatchlistsMovies>>(watchlistsMovies => 
                watchlistsMovies.Single().WatchlistId.Equals(watchlistId) && watchlistsMovies.Single().MovieId.Equals("movieId"))));
            _moviesRepositoryMock.Setup(m => m.GetMoviesByIdReadOnly(It.Is<IEnumerable<string>>(movieIds => movieIds.Single().Equals("movieId")))).ReturnsAsync(moviesRepositoryResult);

            var result = await _watchlistsService.CreateWatchlist(createWatchlistRequest);

            Assert.Equal(watchlistId, result.Id);
            Assert.Equal("watchlist name", result.Name);
            Assert.Equal("movieId", result.Movies.Single().Movie.Id);

            _watchlistsRepositoryMock.Verify(m => m.AddWatchlist(It.Is<Watchlist>(w => w.Name.Equals(createWatchlistRequest.Name))), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.AddWatchlistsMovies(It.Is<IEnumerable<WatchlistsMovies>>(watchlistsMovies =>
                watchlistsMovies.Single().WatchlistId.Equals(watchlistId) && watchlistsMovies.Single().MovieId.Equals("movieId"))), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.SaveChangesAsync(), Times.Once);
            _moviesRepositoryMock.Verify(m => m.GetMoviesByIdReadOnly(It.Is<IEnumerable<string>>(movieIds => movieIds.Single().Equals("movieId"))), Times.Once);
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

            var moviesRepositoryResult = new List<Movie>();

            _watchlistsRepositoryMock.Setup(m => m.AddWatchlist(It.Is<Watchlist>(w => w.Name.Equals(createWatchlistRequest.Name)))).ReturnsAsync(createdWatchlist);
            _watchlistsRepositoryMock.Setup(m => m.AddWatchlistsMovies(It.Is<IEnumerable<WatchlistsMovies>>(watchlistsMovies =>
                watchlistsMovies.Any() == false)));
            _moviesRepositoryMock.Setup(m => m.GetMoviesByIdReadOnly(It.Is<IEnumerable<string>>(movieIds => movieIds.Any() == false))).ReturnsAsync(moviesRepositoryResult);

            var result = await _watchlistsService.CreateWatchlist(createWatchlistRequest);

            Assert.Equal(watchlistId, result.Id);
            Assert.Equal(defaultName, result.Name);
            Assert.Empty(result.Movies);

            _watchlistsRepositoryMock.Verify(m => m.AddWatchlist(It.Is<Watchlist>(w => w.Name.Equals(createWatchlistRequest.Name))), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.AddWatchlistsMovies(It.Is<IEnumerable<WatchlistsMovies>>(watchlistsMovies =>
                watchlistsMovies.Any() == false)), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.SaveChangesAsync(), Times.Once);
            _moviesRepositoryMock.Verify(m => m.GetMoviesByIdReadOnly(It.Is<IEnumerable<string>>(movieIds => movieIds.Any() == false)), Times.Once);
        }

        [Fact]
        public async Task GetWatchlist_ReturnsWatchlist()
        {
            var watchlistId = Guid.NewGuid();

            var watchlist = new Watchlist
            {
                Id = watchlistId,
                Name = "watchlist name"
            };

            var watchlistsMovies = new List<WatchlistsMovies>() { new WatchlistsMovies { WatchlistId = watchlistId, MovieId = "movieId" } };

            var moviesRepositoryResult = new List<Movie> { new Movie { Id = "movieId" } };

            _watchlistsRepositoryMock.Setup(m => m.GetWatchlistById(watchlistId)).ReturnsAsync(watchlist);
            _watchlistsRepositoryMock.Setup(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId)).ReturnsAsync(watchlistsMovies);
            _moviesRepositoryMock.Setup(m => m.GetMoviesByIdReadOnly(It.Is<IEnumerable<string>>(movieIds => movieIds.Single().Equals("movieId")))).ReturnsAsync(moviesRepositoryResult);

            var result = await _watchlistsService.GetWatchlist(watchlistId);

            Assert.Equal(watchlist.Id, result!.Id);
            Assert.Equal(watchlist.Name, result.Name);
            Assert.Equal("movieId", result.Movies.Single().Movie.Id);

            _watchlistsRepositoryMock.Verify(m => m.GetWatchlistById(watchlistId), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId), Times.Once);
            _moviesRepositoryMock.Verify(m => m.GetMoviesByIdReadOnly(It.Is<IEnumerable<string>>(movieIds => movieIds.Single().Equals("movieId"))), Times.Once);
        }

        [Fact]
        public async Task GetWatchlist_WhenWatchlistNull_ReturnsNull()
        {
            var watchlistId = Guid.NewGuid();

            _watchlistsRepositoryMock.Setup(m => m.GetWatchlistById(watchlistId)).ReturnsAsync((Watchlist?)null);

            var result = await _watchlistsService.GetWatchlist(watchlistId);

            Assert.Null(result);

            _watchlistsRepositoryMock.Verify(m => m.GetWatchlistById(watchlistId), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.GetWatchlistsMoviesByWatchlistId(It.IsAny<Guid>()), Times.Never);
            _moviesRepositoryMock.Verify(m => m.GetMoviesByIdReadOnly(It.IsAny<IEnumerable<string>>()), Times.Never);
        }

        [Fact]
        public async Task DeleteWatchlist_RemovesWatchlistMoviesAndWatchlist()
        {
            var watchlistId = Guid.NewGuid();
            var watchlistsMovies = new List<WatchlistsMovies>();

            _watchlistsRepositoryMock.Setup(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId)).ReturnsAsync(watchlistsMovies);
            _watchlistsRepositoryMock.Setup(m => m.RemoveWatchlistsMovies(watchlistsMovies));
            _watchlistsRepositoryMock.Setup(m => m.RemoveWatchlist(watchlistId));

            await _watchlistsService.DeleteWatchlist(watchlistId);

            _watchlistsRepositoryMock.Verify(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.RemoveWatchlistsMovies(watchlistsMovies), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.RemoveWatchlist(watchlistId), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.SaveChangesAsync(), Times.Exactly(2));
        }

        [Fact]
        public async Task AddMoviesToWatchlist_AddsWatchlistMovies()
        {
            var addMoviesToWatchlistRequest = new AddMoviesToWatchlistRequest
            {
                MovieIds = new List<string> { "movieId" }
            };
            var watchlistId = Guid.NewGuid();

            _watchlistsRepositoryMock.Setup(m => m.AddWatchlistsMovies(It.Is<IEnumerable<WatchlistsMovies>>(watchlistsMovies =>
                watchlistsMovies.Single().WatchlistId.Equals(watchlistId) && watchlistsMovies.Single().MovieId.Equals("movieId")))).Returns(Task.CompletedTask);

            await _watchlistsService.AddMoviesToWatchlist(watchlistId, addMoviesToWatchlistRequest);

            _watchlistsRepositoryMock.Verify(m => m.AddWatchlistsMovies(It.Is<IEnumerable<WatchlistsMovies>>(watchlistsMovies =>
                watchlistsMovies.Single().WatchlistId.Equals(watchlistId) && watchlistsMovies.Single().MovieId.Equals("movieId"))), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RemoveMoviesFromWatchlist_RemovesMoviesFromWatchlist()
        {
            var removeMoviesFromWatchlistRequest = new RemoveMoviesFromWatchlistRequest
            {
                MovieIds = new List<string> { "movieId" }
            };
            var watchlistId = Guid.NewGuid();
            var watchlistsMovies = new List<WatchlistsMovies> { new WatchlistsMovies { WatchlistId = watchlistId, MovieId = "movieId" } };

            _watchlistsRepositoryMock.Setup(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId)).ReturnsAsync(watchlistsMovies);

            _watchlistsRepositoryMock.Setup(m => m.RemoveWatchlistsMovies(It.Is<IEnumerable<WatchlistsMovies>>(watchlistsMovies =>
                watchlistsMovies.Single().WatchlistId.Equals(watchlistId) && watchlistsMovies.Single().MovieId.Equals("movieId"))));

            await _watchlistsService.RemoveMoviesFromWatchlist(watchlistId, removeMoviesFromWatchlistRequest);

            _watchlistsRepositoryMock.Verify(m => m.RemoveWatchlistsMovies(It.Is<IEnumerable<WatchlistsMovies>>(watchlistsMovies =>
                watchlistsMovies.Single().WatchlistId.Equals(watchlistId) && watchlistsMovies.Single().MovieId.Equals("movieId"))), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RemoveMoviesFromWatchlist_GivenMovieIdsNotInWatchlist_Throws()
        {
            var removeMoviesFromWatchlistRequest = new RemoveMoviesFromWatchlistRequest
            {
                MovieIds = new List<string> { "invalidMovieId" }
            };
            var watchlistId = Guid.NewGuid();
            var watchlistsMovies = new List<WatchlistsMovies> { new WatchlistsMovies { WatchlistId = watchlistId, MovieId = "movieId" } };

            _watchlistsRepositoryMock.Setup(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId)).ReturnsAsync(watchlistsMovies);

            var exception = await Assert.ThrowsAsync<InvalidRequestException>(async () => await _watchlistsService.RemoveMoviesFromWatchlist(watchlistId, removeMoviesFromWatchlistRequest));

            Assert.Equal("The following movie Ids in the request are invalid: invalidMovieId", exception.Message);

            _watchlistsRepositoryMock.Verify(m => m.RemoveWatchlistsMovies(It.IsAny<IEnumerable<WatchlistsMovies>>()), Times.Never);
            _watchlistsRepositoryMock.Verify(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.SaveChangesAsync(), Times.Never);
        }


        [Fact]
        public async Task SetMoviesAsWatched_SetsRequestedMoviesAsWatched()
        {
            var setMoviesAsWatchedRequest = new SetMoviesWatchedStatusRequest
            {
                MovieIdsWatched = new Dictionary<string, bool> { ["movieId"] = true, ["movieId2"] = false }
            };
            var watchlistId = Guid.NewGuid();

            var watchlistsMovies = new List<WatchlistsMovies> { 
                new WatchlistsMovies { WatchlistId = watchlistId, MovieId = "movieId", Watched = false },
                new WatchlistsMovies { WatchlistId = watchlistId, MovieId = "movieId2", Watched = true },
                new WatchlistsMovies { WatchlistId = watchlistId, MovieId = "movieId3", Watched = false }
            };

            _watchlistsRepositoryMock.Setup(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId)).ReturnsAsync(watchlistsMovies);

            await _watchlistsService.SetMoviesAsWatched(watchlistId, setMoviesAsWatchedRequest);

            Assert.True(watchlistsMovies[0].Watched);
            Assert.False(watchlistsMovies[1].Watched);
            Assert.False(watchlistsMovies[2].Watched);

            _watchlistsRepositoryMock.Verify(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task SetMoviesAsWatched_GivenMovieIdsNotInWatchlist_Throws()
        {
            var setMoviesAsWatchedRequest = new SetMoviesWatchedStatusRequest
            {
                MovieIdsWatched = new Dictionary<string, bool> { ["invalidMovieId"] = true, ["invalidMovieId2"] = false }
            };
            var watchlistId = Guid.NewGuid();

            var watchlistsMovies = new List<WatchlistsMovies> {
                new WatchlistsMovies { WatchlistId = watchlistId, MovieId = "movieId", Watched = false },
                new WatchlistsMovies { WatchlistId = watchlistId, MovieId = "movieId2", Watched = true },
                new WatchlistsMovies { WatchlistId = watchlistId, MovieId = "movieId3", Watched = false }
            };

            _watchlistsRepositoryMock.Setup(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId)).ReturnsAsync(watchlistsMovies);

            var exception = await Assert.ThrowsAsync<InvalidRequestException>(async () => await _watchlistsService.SetMoviesAsWatched(watchlistId, setMoviesAsWatchedRequest));

            Assert.Equal("The following movie Ids in the request are invalid: invalidMovieId,invalidMovieId2", exception.Message);

            _watchlistsRepositoryMock.Verify(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Rename()
        {
            var watchlistId = Guid.NewGuid();
            var name = "newName";

            await _watchlistsService.Rename(watchlistId, name);

            _watchlistsRepositoryMock.Verify(m => m.RenameWatchlist(watchlistId, name), Times.Once);
            _watchlistsRepositoryMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }
    }
}
