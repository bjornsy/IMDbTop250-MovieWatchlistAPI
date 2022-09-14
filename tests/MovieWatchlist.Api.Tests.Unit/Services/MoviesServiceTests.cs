using Moq;
using MovieWatchlist.Api.Services;
using MovieWatchlist.ApplicationCore.Models;
using MovieWatchlist.Infrastructure.Data;
using Xunit;

namespace MovieWatchlist.Api.Tests.Unit.Services
{
    public class MoviesServiceTests
    {
        private Mock<IMoviesRepository> _moviesRepository;
        private MoviesService _moviesService;

        public MoviesServiceTests()
        {
            _moviesRepository = new Mock<IMoviesRepository>();
            _moviesService = new MoviesService(_moviesRepository.Object);
        }

        [Fact]
        public async Task GetTop250_ReturnsFromRepository()
        {
            var movies = Enumerable.Empty<Movie>();
            _moviesRepository.Setup(r => r.GetTop250()).ReturnsAsync(movies);

            var result = await _moviesService.GetTop250();

            Assert.Equal(movies, result);
        }
    }
}
