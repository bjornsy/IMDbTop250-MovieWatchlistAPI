﻿using Microsoft.Extensions.Caching.Memory;
using MovieWatchlist.Api.Extensions;
using MovieWatchlist.Api.Models.Responses;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.Api.Services
{
    public interface IMoviesService
    {
        Task<IReadOnlyCollection<MovieResponse>> GetTop250();
        Task<IReadOnlyCollection<MovieInWatchlistResponse>?> GetMoviesByWatchlistId(Guid watchlistId);
    }

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

        public async Task<IReadOnlyCollection<MovieInWatchlistResponse>?> GetMoviesByWatchlistId(Guid watchlistId)
        {
            var watchlist = await _watchlistsRepository.GetWatchlistById(watchlistId);
            if (watchlist is null)
            {
                return null;
            }

            var watchlistsMovies = await _moviesRepository.GetWatchlistsMoviesByWatchlistId(watchlistId);

            if (watchlistsMovies.Any())
            {
                //TODO: Get movies individually by Id rather than load all
                var movies = await _moviesRepository.GetAllMoviesReadOnly();

                var moviesInWatchlist = movies.Join(watchlistsMovies, m => m.Id, wm => wm.MovieId, (m, wm) => new { Movie = m, WatchlistsMovies = wm })
                                .Select(x => new MovieInWatchlist(x.Movie, x.WatchlistsMovies.Watched));

                return moviesInWatchlist.Select(miw => miw.MapToResponse()).ToList();

            }

            return new List<MovieInWatchlistResponse>();
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