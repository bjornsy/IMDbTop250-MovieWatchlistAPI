using Microsoft.EntityFrameworkCore;
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

        public async Task<Watchlist> AddWatchlist(Watchlist watchlist)
        {
            await _context.Watchlists.AddAsync(watchlist);

            return watchlist;
        }

        public async Task AddWatchlistsMovies(IEnumerable<WatchlistsMovies> watchlistsMovies)
        {
            await _context.WatchlistsMovies.AddRangeAsync(watchlistsMovies);
        }

        public async Task<Watchlist?> GetWatchlistById(Guid watchlistId)
        {
            return await _context.Watchlists.AsNoTracking().SingleOrDefaultAsync(w => w.Id.Equals(watchlistId));
        }

        public async Task<IReadOnlyCollection<WatchlistsMovies>> GetWatchlistsMoviesByWatchlistId(Guid watchlistId)
        {
            return await _context.WatchlistsMovies.Where(wm => wm.WatchlistId.Equals(watchlistId)).ToListAsync();
        }

        public void RemoveWatchlistsMovies(IEnumerable<WatchlistsMovies> watchlistsMovies)
        {
            _context.WatchlistsMovies.RemoveRange(watchlistsMovies);
        }

        public void RemoveWatchlist(Guid watchlistId)
        {
            var stub = new Watchlist { Id = watchlistId };
            _context.Watchlists.Remove(stub);
        }

        public void RenameWatchlist(Guid watchlistId, string name)
        {
            var watchlist = _context.Watchlists.Single(w => w.Id.Equals(watchlistId));

            watchlist.Name = name;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
