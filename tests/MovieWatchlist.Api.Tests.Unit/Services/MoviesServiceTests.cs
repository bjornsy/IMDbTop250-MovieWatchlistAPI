using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using MovieWatchlist.Api.Services;
using MovieWatchlist.ApplicationCore.Interfaces.Clients;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;
using System.Collections.ObjectModel;
using Xunit;

namespace MovieWatchlist.Api.Tests.Unit.Services
{
    public class MoviesServiceTests
    {
        private Mock<ITop250InfoService> _top250InfoServiceMock;
        private Mock<IMoviesRepository> _moviesRepositoryMock;
        private Mock<ITop250MoviesDatabaseUpdateService> _top250MoviesDatabaseUpdateServiceMock;
        private IMemoryCache _memoryCache;
        private Mock<ILogger<MoviesService>> _loggerMock;
        private MoviesService _moviesService;

        public MoviesServiceTests()
        {
            _moviesRepositoryMock = new Mock<IMoviesRepository>();
            _top250InfoServiceMock = new Mock<ITop250InfoService>();
            _top250MoviesDatabaseUpdateServiceMock = new Mock<ITop250MoviesDatabaseUpdateService>();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _loggerMock = new Mock<ILogger<MoviesService>>();
            _moviesService = new MoviesService(_top250InfoServiceMock.Object, _moviesRepositoryMock.Object, _top250MoviesDatabaseUpdateServiceMock.Object, _memoryCache, _loggerMock.Object);
        }

        [Fact]
        public async Task GetTop250_ReturnsMovieResponsesFromTop250InfoService()
        {
            var movies = new ReadOnlyCollection<Movie>(new List<Movie> { new Movie { Id = "1", Title = "Title", Ranking = 1, Rating = 1 } });
            _top250InfoServiceMock.Setup(m => m.GetTop250()).ReturnsAsync(movies);

            var result = await _moviesService.GetTop250();

            Assert.Equal(movies.Single().Id, result.Single().Id);
            Assert.Equal(movies.Single().Title, result.Single().Title);
            Assert.Equal(movies.Single().Ranking, result.Single().Ranking);
            Assert.Equal(movies.Single().Rating, result.Single().Rating);

            _top250MoviesDatabaseUpdateServiceMock.Verify(m => m.UpdateTop250InDatabase(movies), Times.Once);
            _moviesRepositoryMock.Verify(m => m.GetAllMoviesReadOnly(), Times.Never);
            _loggerMock.Verify(logger => logger.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Never);

        }

        [Fact]
        public async Task GetTop250_ReturnsMovieResponsesFromMemoryCache()
        {
            var movies = new ReadOnlyCollection<Movie>(new List<Movie> { new Movie { Id = "1" } });
            _top250InfoServiceMock.Setup(m => m.GetTop250()).ReturnsAsync(movies);

            var firstCallResult = await _moviesService.GetTop250();
            var secondCallResult = await _moviesService.GetTop250();

            Assert.Equal(movies.Single().Id, firstCallResult.Single().Id);
            Assert.Same(movies.Single().Id, secondCallResult.Single().Id);

            _top250InfoServiceMock.Verify(m => m.GetTop250(), Times.Once);
            _top250MoviesDatabaseUpdateServiceMock.Verify(m => m.UpdateTop250InDatabase(movies), Times.Once);
            _moviesRepositoryMock.Verify(m => m.GetAllMoviesReadOnly(), Times.Never);
            _loggerMock.Verify(logger => logger.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Never);

        }

        [Fact]
        public async Task GetTop250_WhenTop250InfoServiceThrows_ReturnsMovieResponses()
        {
            var exception = new Exception();
            _top250InfoServiceMock.Setup(m => m.GetTop250()).ThrowsAsync(exception);

            var movies = GenerateMoreThan250Movies();
            _moviesRepositoryMock.Setup(m => m.GetAllMoviesReadOnly()).ReturnsAsync(movies);

            var result = await _moviesService.GetTop250();

            Assert.Equal(250, result.Count);
            var resultAsList = result.ToList();
            for (var i = 0; i < result.Count; i++)
            {
                Assert.Same(movies[i].Id, resultAsList[i].Id);
                Assert.Equal(movies[i].Ranking, resultAsList[i].Ranking);
            }

            _top250InfoServiceMock.Verify(m => m.GetTop250(), Times.Once);
            _top250MoviesDatabaseUpdateServiceMock.Verify(m => m.UpdateTop250InDatabase(It.IsAny<IReadOnlyCollection<Movie>>()), Times.Never);
            _moviesRepositoryMock.Verify(m => m.GetAllMoviesReadOnly(), Times.Once);
            _loggerMock.Verify(logger => logger.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Equals("Error getting movies from client, using repository as fallback")),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }

        private List<Movie> GenerateMoreThan250Movies()
        {
            var movies = new List<Movie>();
            for (var i = 1; i < 300; i++)
            {
                movies.Add(new Movie { Id = i.ToString(), Ranking = i });
            }

            return movies;
        }

        [Fact]
        public async Task GetMoviesByWatchlistId_ReturnsJoinedMovieInWatchlistResponses()
        {
            var watchlistId = Guid.NewGuid();
            var movies = new List<Movie> { 
                new Movie { Id = "1", Title = "Title", Ranking = 1, Rating = 1 },
                new Movie { Id = "2", Title = "Title2", Ranking = 2, Rating = 2 } 
            };
            var watchlistsMovies = new List<WatchlistsMovies> { 
                new WatchlistsMovies { Id = 1, WatchlistId = watchlistId, MovieId = "1", Watched = true },
            };

            _moviesRepositoryMock.Setup(m => m.GetAllMoviesReadOnly()).ReturnsAsync(movies);
            _moviesRepositoryMock.Setup(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId)).ReturnsAsync(watchlistsMovies);

            var result = await _moviesService.GetMoviesByWatchlistId(watchlistId);

            Assert.Equal("1", result.Single().Movie.Id);
            Assert.Equal("Title", result.Single().Movie.Title);
            Assert.Equal(1, result.Single().Movie.Rating);
            Assert.Equal(1, result.Single().Movie.Ranking);
            Assert.True(result.Single().Watched);

            _moviesRepositoryMock.Verify(m => m.GetAllMoviesReadOnly(), Times.Once);
            _moviesRepositoryMock.Verify(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId), Times.Once);
        }

        [Fact]
        public async Task GetMoviesByWatchlistId_WhenNoMoviesInWatchlist_ReturnsEmptyMoviesInWatchlistResponses()
        {
            var watchlistId = Guid.NewGuid();
            var movies = new List<Movie> {
                new Movie { Id = "1", Title = "Title", Ranking = 1, Rating = 1 },
                new Movie { Id = "2", Title = "Title2", Ranking = 2, Rating = 2 }
            };
            var watchlistsMovies = new List<WatchlistsMovies> { };

            _moviesRepositoryMock.Setup(m => m.GetAllMoviesReadOnly()).ReturnsAsync(movies);
            _moviesRepositoryMock.Setup(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId)).ReturnsAsync(watchlistsMovies);

            var result = await _moviesService.GetMoviesByWatchlistId(watchlistId);

            Assert.Equal(0, result.Count);

            _moviesRepositoryMock.Verify(m => m.GetAllMoviesReadOnly(), Times.Once);
            _moviesRepositoryMock.Verify(m => m.GetWatchlistsMoviesByWatchlistId(watchlistId), Times.Once);
        }
    }
}
