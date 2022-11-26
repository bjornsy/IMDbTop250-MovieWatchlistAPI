using Microsoft.EntityFrameworkCore;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;
using System.Linq;

namespace MovieWatchlist.Infrastructure.Data
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly MovieWatchlistContext _context;

        public WatchlistRepository(MovieWatchlistContext context)
        {
            _context = context;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
