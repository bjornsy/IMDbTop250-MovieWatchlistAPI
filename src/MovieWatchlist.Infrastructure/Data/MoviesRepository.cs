using Microsoft.EntityFrameworkCore;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.Infrastructure.Data
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly MovieWatchlistContext _context;

        public MoviesRepository(MovieWatchlistContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Movie>> GetTop250()
        {
            return await _context.Movies.Where(m => m.Ranking < 251).OrderBy(m => m.Ranking).ToListAsync();
        }
    }
}
