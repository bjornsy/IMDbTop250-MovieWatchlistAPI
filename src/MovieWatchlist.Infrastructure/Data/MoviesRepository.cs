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
            return await _context.Movies.AsNoTracking().Where(m => m.Ranking != null && m.Ranking < 251).OrderBy(m => m.Ranking).ToListAsync();
        }

        public async Task<IList<Movie>> GetAll()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<IReadOnlyCollection<MovieInWatchlist>> GetMoviesByWatchlistId(Guid watchlistId)
        {
            return await _context.Movies.AsNoTracking()
                .Join(_context.WatchlistsMovies.AsNoTracking().Where(wm => wm.WatchlistId.Equals(watchlistId)),
                    m => m.Id, wm => wm.MovieId, (m, wm) => new { Movie = m, WatchlistsMovies = wm })
                .Select(x => new MovieInWatchlist(x.Movie, x.WatchlistsMovies.Watched))
                .ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
