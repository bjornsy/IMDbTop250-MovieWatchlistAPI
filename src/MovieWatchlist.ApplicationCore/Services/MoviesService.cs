﻿using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;
using Microsoft.Extensions.Logging;
using MovieWatchlist.ApplicationCore.Interfaces.Services;
using MovieWatchlist.ApplicationCore.Models.DTO;
using MovieWatchlist.ApplicationCore.Mapping;

namespace MovieWatchlist.ApplicationCore.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly ITop250InfoService _top250InfoService;
        private readonly IMoviesRepository _moviesRepository;
        private readonly ITop250MoviesDatabaseUpdateService _top250MoviesDatabaseUpdateService;
        private readonly ILogger<MoviesService> _logger;

        public MoviesService(ITop250InfoService top250InfoService,
            IMoviesRepository moviesRepository,
            ITop250MoviesDatabaseUpdateService top250MoviesDatabaseUpdateService, 
            ILogger<MoviesService> logger)
        {
            _top250InfoService = top250InfoService;
            _moviesRepository = moviesRepository;
            _top250MoviesDatabaseUpdateService = top250MoviesDatabaseUpdateService;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<MovieDTO>> GetTop250(CancellationToken cancellationToken)
        {
            try
            {
                var movies = await GetTop250FromClientAndUpdateDb();
                return movies;
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting movies from client, using repository as fallback");

                var movies = await _moviesRepository.GetAllMoviesReadOnly(cancellationToken);
                var top250 = GetTop250(movies);

                return top250;
            }
        }

        public async Task<IReadOnlyCollection<MovieDTO>> GetMovies(IEnumerable<string> movieIds, CancellationToken cancellationToken)
        {
            var movies = await _moviesRepository.GetMoviesByIdReadOnly(movieIds, cancellationToken);
            return movies.Select(m => m.MapToDTO()).ToList();
        }

        private async Task<IReadOnlyCollection<MovieDTO>> GetTop250FromClientAndUpdateDb()
        {
            var movies = await _top250InfoService.GetTop250();

            await _top250MoviesDatabaseUpdateService.UpdateTop250InDatabase(movies);

            return movies.Select(m => m.MapToDTO()).ToList();
        }

        private static IReadOnlyCollection<MovieDTO> GetTop250(IEnumerable<Movie> movies)
        {
            return movies.Where(m => m.Ranking != null && m.Ranking < 251)
                .OrderBy(m => m.Ranking)
                .Select(m => m.MapToDTO())
                .ToList();
        }
    }
}