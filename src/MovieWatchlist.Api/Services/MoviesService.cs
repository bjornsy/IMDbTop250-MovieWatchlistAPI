using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.Api.Services
{
    public interface IMoviesService
    {
        Task<IReadOnlyCollection<Movie>> GetTop250();
    }

    public class MoviesService : IMoviesService
    {
        private readonly IMoviesRepository _moviesRepository;

        public MoviesService(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public async Task<IReadOnlyCollection<Movie>> GetTop250()
        {
            return await _moviesRepository.GetTop250();
        }
    }
}