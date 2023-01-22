﻿using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Interfaces.Data
{
    public interface IMoviesRepository
    {
        Task<IList<Movie>> GetAllMovies();
        Task<IReadOnlyCollection<Movie>> GetAllMoviesReadOnly();
        Task<IReadOnlyCollection<WatchlistsMovies>> GetWatchlistsMoviesByWatchlistId(Guid watchlistId);
        Task AddMovie(Movie movie);
        Task SaveChangesAsync();
    }
}
