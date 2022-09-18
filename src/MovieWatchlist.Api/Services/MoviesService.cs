using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<MoviesService> _logger;

        private const string CacheKey = "Top250Info";

        public MoviesService(ITop250InfoService top250InfoService, IMoviesRepository moviesRepository, IMemoryCache memoryCache, ILogger<MoviesService> logger)
        {
            _top250InfoService = top250InfoService;
            _moviesRepository = moviesRepository;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<Movie>> GetTop250()
        {
            try
            {
                return await _memoryCache.GetOrCreateAsync<IReadOnlyCollection<Movie>>(CacheKey, async cacheEntry =>
                {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
                    return await GetTop250FromClientAndUpdateDb();
                });
            } 
            catch (Exception ex)
            {
                _logger.LogError("Error getting movies from client, using repository as fallback", ex);

                return await _moviesRepository.GetTop250();
            }
        }

        private async Task<IReadOnlyCollection<Movie>> GetTop250FromClientAndUpdateDb()
        {
            return await _top250InfoService.GetTop250();
        }
    }
}