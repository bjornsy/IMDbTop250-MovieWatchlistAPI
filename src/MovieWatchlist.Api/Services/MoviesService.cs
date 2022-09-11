using MovieWatchlist.ApplicationCore.Models;
using MovieWatchlist.Infrastructure.Data;

namespace MovieWatchlist.Api.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetTop250();
    }

    public class MoviesService : IMoviesService
    {
        private readonly IMovieWatchlistRepository _moviesRepository;

        public MoviesService(IMovieWatchlistRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public async Task<IEnumerable<Movie>> GetTop250()
        {
            throw new NotImplementedException();
        }
    }
}