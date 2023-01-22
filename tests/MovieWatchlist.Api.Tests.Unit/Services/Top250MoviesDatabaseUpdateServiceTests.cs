using Moq;
using MovieWatchlist.Api.Services;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;
using Xunit;

namespace MovieWatchlist.Api.Tests.Unit.Services
{
    public class Top250MoviesDatabaseUpdateServiceTests
    {
        private readonly Mock<IMoviesRepository> _moviesRepositoryMock;
        private readonly Top250MoviesDatabaseUpdateService _databaseUpdateService;

        public Top250MoviesDatabaseUpdateServiceTests()
        {
            _moviesRepositoryMock = new Mock<IMoviesRepository>();
            _databaseUpdateService = new Top250MoviesDatabaseUpdateService(_moviesRepositoryMock.Object);
        }

        [Fact]
        public async Task UpdateTop250InDatabase_GivenRankingChanges_UpdatesRanking()
        {
            var dbMovies = new List<Movie>
            {
                new Movie
                {
                    Id = "0111161",
                    Ranking = 1,
                    Title = "The Shawshank Redemption (1994)",
                    Rating = 9.2M
                },
                new Movie
                {
                    Id = "0068646",
                    Ranking = 2,
                    Title = "The Godfather (1972)",
                    Rating = 9.2M
                }
            };

            var updatedMovies = new List<Movie>
            {
                new Movie
                {
                    Id = "0111161",
                    Ranking = 2,
                    Title = "The Shawshank Redemption (1994)",
                    Rating = 9.2M
                },
                new Movie
                {
                    Id = "0068646",
                    Ranking = 1,
                    Title = "The Godfather (1972)",
                    Rating = 9.2M
                }
            };

            _moviesRepositoryMock.Setup(r => r.GetAllMovies()).ReturnsAsync(dbMovies);

            await _databaseUpdateService.UpdateTop250InDatabase(updatedMovies);

            var dbMoviesShawshank = dbMovies.Single(m => m.Id.Equals("0111161"));
            var updatedMoviesShawshank = updatedMovies.Single(m => m.Id.Equals("0111161"));

            var dbMoviesGodfather = dbMovies.Single(m => m.Id.Equals("0068646"));
            var updatedMoviesGodfather = updatedMovies.Single(m => m.Id.Equals("0068646"));

            Assert.Equal(updatedMoviesShawshank.Ranking, dbMoviesShawshank.Ranking);
            Assert.Equal(updatedMoviesGodfather.Ranking, dbMoviesGodfather.Ranking);

            _moviesRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateTop250InDatabase_GivenRatingChanges_UpdatesRating()
        {
            var dbMovies = new List<Movie>
            {
                new Movie
                {
                    Id = "0111161",
                    Ranking = 1,
                    Title = "The Shawshank Redemption (1994)",
                    Rating = 9.1M
                },
                new Movie
                {
                    Id = "0068646",
                    Ranking = 2,
                    Title = "The Godfather (1972)",
                    Rating = 9.0M
                }
            };

            var updatedMovies = new List<Movie>
            {
                new Movie
                {
                    Id = "0111161",
                    Ranking = 2,
                    Title = "The Shawshank Redemption (1994)",
                    Rating = 9.1M
                },
                new Movie
                {
                    Id = "0068646",
                    Ranking = 1,
                    Title = "The Godfather (1972)",
                    Rating = 9.0M
                }
            };

            _moviesRepositoryMock.Setup(r => r.GetAllMovies()).ReturnsAsync(dbMovies);

            await _databaseUpdateService.UpdateTop250InDatabase(updatedMovies);

            var dbMoviesShawshank = dbMovies.Single(m => m.Id.Equals("0111161"));
            var updatedMoviesShawshank = updatedMovies.Single(m => m.Id.Equals("0111161"));

            var dbMoviesGodfather = dbMovies.Single(m => m.Id.Equals("0068646"));
            var updatedMoviesGodfather = updatedMovies.Single(m => m.Id.Equals("0068646"));

            Assert.Equal(updatedMoviesShawshank.Rating, dbMoviesShawshank.Rating);
            Assert.Equal(updatedMoviesGodfather.Rating, dbMoviesGodfather.Rating);

            _moviesRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateTop250InDatabase_GivenMovieNoLongerInList_SetsRankingToNull()
        {
            var dbMovies = new List<Movie>
            {
                new Movie
                {
                    Id = "0129167",
                    Ranking = 250,
                    Title = "The Iron Giant (1999)",
                    Rating = 8.0M
                }
            };

            var updatedMovies = new List<Movie>
            {

            };

            _moviesRepositoryMock.Setup(r => r.GetAllMovies()).ReturnsAsync(dbMovies);

            await _databaseUpdateService.UpdateTop250InDatabase(updatedMovies);

            Assert.Null(dbMovies.Single().Ranking);

            _moviesRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateTop250InDatabase_GivenNewMovieInList_AddsNewMovie()
        {
            var dbMovies = new List<Movie>
            {

            };

            var updatedMovies = new List<Movie>
            {
                new Movie
                {
                    Id = "newMovieId",
                    Ranking = 250,
                    Title = "New Movie",
                    Rating = 8.0M
                }
            };

            _moviesRepositoryMock.Setup(r => r.GetAllMovies()).ReturnsAsync(dbMovies);

            await _databaseUpdateService.UpdateTop250InDatabase(updatedMovies);

            Assert.Same(updatedMovies.Single(), dbMovies.Single());

            _moviesRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
