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
        private Mock<ITop250InfoService> _top250InfoService;
        private Mock<IMoviesRepository> _moviesRepository;
        private Mock<ILogger<MoviesService>> _loggerMock;
        private MoviesService _moviesService;

        public MoviesServiceTests()
        {
            _moviesRepository = new Mock<IMoviesRepository>();
            _top250InfoService = new Mock<ITop250InfoService>();
            _loggerMock = new Mock<ILogger<MoviesService>>();
            _moviesService = new MoviesService(_top250InfoService.Object, _moviesRepository.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetTop250_ReturnsFromTop250InfoService()
        {
            var movies = new ReadOnlyCollection<Movie>(new List<Movie>());
            _top250InfoService.Setup(r => r.GetTop250()).ReturnsAsync(movies);

            var result = await _moviesService.GetTop250();

            Assert.Equal(movies, result);

            _moviesRepository.Verify(repository => repository.GetTop250(), Times.Never);
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
            _top250InfoService.Setup(r => r.GetTop250()).ThrowsAsync(exception);

            var movies = new ReadOnlyCollection<Movie>(new List<Movie>());
            _moviesRepository.Setup(r => r.GetTop250()).ReturnsAsync(movies);

            var result = await _moviesService.GetTop250();

            Assert.Equal(movies, result);

            _top250InfoService.Verify(service => service.GetTop250(), Times.Once);
            _moviesRepository.Verify(repository => repository.GetTop250(), Times.Once);
            _loggerMock.Verify(logger => logger.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Equals("Error getting movies from client, using repository as fallback")),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }
    }
}
