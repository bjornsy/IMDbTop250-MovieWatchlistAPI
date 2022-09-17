using MovieWatchlist.ApplicationCore.Interfaces.Clients;
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
        private readonly ITop250InfoService _top250InfoService;
        private readonly IMoviesRepository _moviesRepository;
        private readonly ILogger<MoviesService> _logger;

        public MoviesService(ITop250InfoService top250InfoService, IMoviesRepository moviesRepository, ILogger<MoviesService> logger)
        {
            _top250InfoService = top250InfoService;
            _moviesRepository = moviesRepository;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<Movie>> GetTop250()
        {
            try
            {
                return await _top250InfoService.GetTop250();
            } 
            catch (Exception ex)
            {
                _logger.LogError("Error getting movies from client, using repository as fallback", ex);

                return await _moviesRepository.GetTop250();
            }
        }
    }
}