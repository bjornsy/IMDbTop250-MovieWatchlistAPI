﻿using Microsoft.EntityFrameworkCore;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.Infrastructure.Data
{
    public class WatchlistsRepository : IWatchlistsRepository
    {
        private readonly MovieWatchlistContext _context;

        public WatchlistsRepository(MovieWatchlistContext context)
        {
            _context = context;
        }

        public async Task<Watchlist> SaveWatchlist(Watchlist watchlist, List<string> movieIds)
        {
            await _context.Watchlists.AddAsync(watchlist);

            var watchlistsMoviesRecords = movieIds.Select(id => new WatchlistsMovies { WatchlistId = watchlist.Id, MovieId = id });
            await _context.WatchlistsMovies.AddRangeAsync(watchlistsMoviesRecords);

            await _context.SaveChangesAsync();

            return watchlist;
        }

        public async Task<Watchlist> GetWatchlistById(Guid watchlistId)
        {
            return await _context.Watchlists.AsNoTracking().SingleAsync(w => w.Id.Equals(watchlistId));
        }

        public async Task DeleteWatchlist(Guid watchlistId)
        {
            var watchlistsMoviesRecords = _context.WatchlistsMovies.AsNoTracking().Where(wm => wm.WatchlistId.Equals(watchlistId));

            _context.WatchlistsMovies.RemoveRange(watchlistsMoviesRecords);

            var watchlist = _context.Watchlists.AsNoTracking().Single(w => w.Id.Equals(watchlistId));

            _context.Watchlists.Remove(watchlist);

            await _context.SaveChangesAsync();
        }

        public async Task AddMoviesToWatchlist(Guid watchlistId, List<string> movieIds)
        {
            var watchlistsMoviesRecords = movieIds.Select(id => new WatchlistsMovies { WatchlistId = watchlistId, MovieId = id });

            await _context.WatchlistsMovies.AddRangeAsync(watchlistsMoviesRecords);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveMoviesFromWatchlist(Guid watchlistId, List<string> movieIds)
        {
            var watchlistsMoviesRecords = movieIds.Select(id => new WatchlistsMovies { WatchlistId = watchlistId, MovieId = id });

            _context.WatchlistsMovies.RemoveRange(watchlistsMoviesRecords);

            await _context.SaveChangesAsync();
        }

        public async Task SetMoviesAsWatched(Guid watchlistId, List<string> movieIds)
        {
            var watchlistMovies = _context.WatchlistsMovies.Where(wm => wm.WatchlistId.Equals(watchlistId) && movieIds.Contains(wm.MovieId));

            //TODO: Error if none/some not found?

            foreach (var watchlistMovie in watchlistMovies)
            {
                watchlistMovie.Watched = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}
