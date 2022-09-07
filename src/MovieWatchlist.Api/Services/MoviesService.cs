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
        private readonly IMoviesRepository _moviesRepository;

        public MoviesService(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public async Task<IEnumerable<Movie>> GetTop250()
        {
            throw new NotImplementedException();
        }
    }
}