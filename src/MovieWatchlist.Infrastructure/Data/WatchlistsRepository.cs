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
            await _context.SaveChangesAsync();

            var watchlistsMoviesRecords = movieIds.Select(id => new WatchlistsMovies { WatchlistId = watchlist.Id, MovieId = id });
            await _context.WatchlistsMovies.AddRangeAsync(watchlistsMoviesRecords);
            await _context.SaveChangesAsync();

            return watchlist;
        }

        public async Task AddMoviesToWatchlist(Guid watchlistId, List<string> movieIds)
        {
            var watchlistsMoviesRecords = movieIds.Select(id => new WatchlistsMovies { WatchlistId = watchlistId, MovieId = id });

            await _context.WatchlistsMovies.AddRangeAsync(watchlistsMoviesRecords);
            await _context.SaveChangesAsync();

        }
    }
}
