using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using MovieWatchlist.Api.Models.Responses;
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
        public async Task GetTop250_ReturnsFromTop250InfoService()
        {
            var movies = new ReadOnlyCollection<Movie>(new List<Movie>());
            _top250InfoServiceMock.Setup(r => r.GetTop250()).ReturnsAsync(movies);

            var result = await _moviesService.GetTop250();

            Assert.Same(movies, result);

            _top250MoviesDatabaseUpdateServiceMock.Verify(s => s.UpdateTop250InDatabase(result), Times.Once);
            _moviesRepositoryMock.Verify(repository => repository.GetTop250(), Times.Never);
            _loggerMock.Verify(logger => logger.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Never);

        }

        [Fact]
        public async Task GetTop250_ReturnsFromMemoryCache()
        {
            var movies = new ReadOnlyCollection<Movie>(new List<Movie>());
            _top250InfoServiceMock.Setup(r => r.GetTop250()).ReturnsAsync(movies);

            var firstCallResult = await _moviesService.GetTop250();
            var secondCallResult = await _moviesService.GetTop250();

            Assert.Same(movies, firstCallResult);
            Assert.Same(movies, secondCallResult);

            _top250InfoServiceMock.Verify(r => r.GetTop250(), Times.Once);
            _top250MoviesDatabaseUpdateServiceMock.Verify(s => s.UpdateTop250InDatabase(firstCallResult), Times.Once);
            _moviesRepositoryMock.Verify(repository => repository.GetTop250(), Times.Never);
            _loggerMock.Verify(logger => logger.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Never);

        }

        [Fact]
        public async Task GetTop250_WhenTop250InfoServiceThrows_ReturnsFromRepository()
        {
            var exception = new Exception();
            _top250InfoServiceMock.Setup(r => r.GetTop250()).ThrowsAsync(exception);

            var movies = new ReadOnlyCollection<Movie>(new List<Movie>());
            _moviesRepositoryMock.Setup(r => r.GetTop250()).ReturnsAsync(movies);

            var result = await _moviesService.GetTop250();

            Assert.Same(movies, result);

            _top250InfoServiceMock.Verify(service => service.GetTop250(), Times.Once);
            _top250MoviesDatabaseUpdateServiceMock.Verify(s => s.UpdateTop250InDatabase(It.IsAny<IReadOnlyCollection<Movie>>()), Times.Never);
            _moviesRepositoryMock.Verify(repository => repository.GetTop250(), Times.Once);
            _loggerMock.Verify(logger => logger.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Equals("Error getting movies from client, using repository as fallback")),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }
    }
}
