using Microsoft.Extensions.Caching.Memory;
using MovieWatchlist.Api.Extensions;
using MovieWatchlist.Api.Models.Responses;
using MovieWatchlist.ApplicationCore.Interfaces.Clients;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.Api.Services
{
    public interface IMoviesService
    {
        Task<IReadOnlyCollection<MovieResponse>> GetTop250();
        Task<IReadOnlyCollection<MovieInWatchlistResponse>> GetMoviesByWatchlistId(Guid watchlistId);
    }

    public class MoviesService : IMoviesService
    {
        private readonly ITop250InfoService _top250InfoService;
        private readonly IMoviesRepository _moviesRepository;
        private readonly ITop250MoviesDatabaseUpdateService _top250MoviesDatabaseUpdateService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<MoviesService> _logger;

        private const string CacheKey = "Top250Info";

        public MoviesService(ITop250InfoService top250InfoService,
            IMoviesRepository moviesRepository,
            ITop250MoviesDatabaseUpdateService top250MoviesDatabaseUpdateService, 
            IMemoryCache memoryCache,
            ILogger<MoviesService> logger)
        {
            _top250InfoService = top250InfoService;
            _moviesRepository = moviesRepository;
            _top250MoviesDatabaseUpdateService = top250MoviesDatabaseUpdateService;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<MovieResponse>> GetTop250()
        {
            try
            {
                return await _memoryCache.GetOrCreateAsync<IReadOnlyCollection<MovieResponse>>(CacheKey, async cacheEntry =>
                {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
                    var movies = await GetTop250FromClientAndUpdateDb();
                    return MapMovies(movies);
                });
            } 
            catch (Exception ex)
            {
                _logger.LogError("Error getting movies from client, using repository as fallback", ex);

                var movies = await _moviesRepository.GetTop250();
                return MapMovies(movies);
            }
        }

        public async Task<IReadOnlyCollection<MovieInWatchlistResponse>> GetMoviesByWatchlistId(Guid watchlistId)
        {
            var moviesInWatchlist = await _moviesRepository.GetMoviesByWatchlistId(watchlistId);

            return moviesInWatchlist.Select(miw => miw.MapToResponse()).ToList();
        }

        private async Task<IReadOnlyCollection<Movie>> GetTop250FromClientAndUpdateDb()
        {
            var movies = await _top250InfoService.GetTop250();

            await _top250MoviesDatabaseUpdateService.UpdateTop250InDatabase(movies);

            return movies;
        }

        private IReadOnlyCollection<MovieResponse> MapMovies(IEnumerable<Movie> movies)
        {
            return movies.Select(m => m.MapToResponse()).ToList();
        }
    }
}