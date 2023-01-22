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

        public async Task<IList<Movie>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<IReadOnlyCollection<Movie>> GetAllMoviesReadOnly()
        {
            return await _context.Movies.AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyCollection<WatchlistsMovies>> GetWatchlistsMoviesByWatchlistId(Guid watchlistId)
        {
            return await _context.WatchlistsMovies.AsNoTracking().Where(wm => wm.WatchlistId.Equals(watchlistId)).ToListAsync();
        }

        public async Task UpdateRange(IEnumerable<Movie> movies)
        {
            //await _context.UpdateRange
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
