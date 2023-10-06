using MovieWatchlist.Contracts.Responses;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using MovieWatchlist.ApplicationCore.Extensions;
using MovieWatchlist.ApplicationCore.Interfaces.Services;

namespace MovieWatchlist.ApplicationCore.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly ITop250InfoService _top250InfoService;
        private readonly IMoviesRepository _moviesRepository;
        private readonly IWatchlistsRepository _watchlistsRepository;
        private readonly ITop250MoviesDatabaseUpdateService _top250MoviesDatabaseUpdateService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<MoviesService> _logger;

        private const string CacheKey = "Top250Info";

        public MoviesService(ITop250InfoService top250InfoService,
            IMoviesRepository moviesRepository,
            IWatchlistsRepository watchlistsRepository,
            ITop250MoviesDatabaseUpdateService top250MoviesDatabaseUpdateService, 
            IMemoryCache memoryCache,
            ILogger<MoviesService> logger)
        {
            _top250InfoService = top250InfoService;
            _moviesRepository = moviesRepository;
            _watchlistsRepository = watchlistsRepository;
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

                var movies = await _moviesRepository.GetAllMoviesReadOnly();
                var top250 = GetTop250(movies);

                return MapMovies(top250);
            }
        }

        public async Task<IReadOnlyCollection<MovieResponse>> GetMovies(IEnumerable<string> movieIds)
        {
            return (await _moviesRepository.GetMoviesByIdReadOnly(movieIds)).Select(m => m.MapToResponse()).ToList();
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

        private IReadOnlyCollection<Movie> GetTop250(IEnumerable<Movie> movies)
        {
            return movies.Where(m => m.Ranking != null && m.Ranking < 251).OrderBy(m => m.Ranking).ToList();
        }
    }
}