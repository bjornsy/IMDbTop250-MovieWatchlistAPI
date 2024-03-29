﻿using Microsoft.Extensions.Logging;
using Moq;
using MovieWatchlist.ApplicationCore.Services;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;
using System.Collections.ObjectModel;
using Xunit;
using MovieWatchlist.ApplicationCore.Interfaces.Services;

namespace MovieWatchlist.ApplicationCore.Tests.Unit.Services
{
    public class MoviesServiceTests
    {
        private Mock<ITop250InfoService> _top250InfoServiceMock;
        private Mock<IMoviesRepository> _moviesRepositoryMock;
        private Mock<IWatchlistsRepository> _watchlistsRepositoryMock;
        private Mock<ITop250MoviesDatabaseUpdateService> _top250MoviesDatabaseUpdateServiceMock;
        private Mock<ILogger<MoviesService>> _loggerMock;
        private MoviesService _moviesService;

        public MoviesServiceTests()
        {
            _moviesRepositoryMock = new Mock<IMoviesRepository>();
            _watchlistsRepositoryMock = new Mock<IWatchlistsRepository>();
            _top250InfoServiceMock = new Mock<ITop250InfoService>();
            _top250MoviesDatabaseUpdateServiceMock = new Mock<ITop250MoviesDatabaseUpdateService>();
            _loggerMock = new Mock<ILogger<MoviesService>>();
            _moviesService = new MoviesService(_top250InfoServiceMock.Object, _moviesRepositoryMock.Object, _top250MoviesDatabaseUpdateServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetTop250_ReturnsMovieResponsesFromTop250InfoService()
        {
            var movies = new ReadOnlyCollection<Movie>(new List<Movie> { new Movie { Id = "1", Title = "Title", Ranking = 1, Rating = 1 } });
            _top250InfoServiceMock.Setup(m => m.GetTop250()).ReturnsAsync(movies);

            var result = await _moviesService.GetTop250(CancellationToken.None);

            Assert.Equal(movies.Single().Id, result.Single().Id);
            Assert.Equal(movies.Single().Title, result.Single().Title);
            Assert.Equal(movies.Single().Ranking, result.Single().Ranking);
            Assert.Equal(movies.Single().Rating, result.Single().Rating);

            _top250MoviesDatabaseUpdateServiceMock.Verify(m => m.UpdateTop250InDatabase(movies), Times.Once);
            _moviesRepositoryMock.Verify(m => m.GetAllMoviesReadOnly(CancellationToken.None), Times.Never);
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
            _moviesRepositoryMock.Setup(m => m.GetAllMoviesReadOnly(CancellationToken.None)).ReturnsAsync(movies);

            var result = await _moviesService.GetTop250(CancellationToken.None);

            Assert.Equal(250, result.Count);
            var resultAsList = result.ToList();
            for (var i = 0; i < result.Count; i++)
            {
                Assert.Same(movies[i].Id, resultAsList[i].Id);
                Assert.Equal(movies[i].Ranking, resultAsList[i].Ranking);
            }

            _top250InfoServiceMock.Verify(m => m.GetTop250(), Times.Once);
            _top250MoviesDatabaseUpdateServiceMock.Verify(m => m.UpdateTop250InDatabase(It.IsAny<IReadOnlyCollection<Movie>>()), Times.Never);
            _moviesRepositoryMock.Verify(m => m.GetAllMoviesReadOnly(CancellationToken.None), Times.Once);
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
        public async Task GetMovies_ReturnsRequestedMovieResponses()
        {
            var movieIds = new List<string>
            {
                "movieId"
            };

            var movies = new List<Movie> { new Movie { Id = "movieId", Title = "movieTitle", Ranking = 1, Rating = 1 } };

            _moviesRepositoryMock.Setup(m => m.GetMoviesByIdReadOnly(movieIds, CancellationToken.None)).ReturnsAsync(movies);

            var result = await _moviesService.GetMovies(movieIds, CancellationToken.None);

            var movie = result.Single();
            Assert.Equal("movieId", movie.Id);
            Assert.Equal("movieTitle", movie.Title);
            Assert.Equal(1, movie.Ranking);
            Assert.Equal(1, movie.Rating);

            _moviesRepositoryMock.Verify(m => m.GetMoviesByIdReadOnly(movieIds, CancellationToken.None), Times.Once);
        }
    }
}
