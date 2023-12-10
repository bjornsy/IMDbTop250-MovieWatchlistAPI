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

        public async Task<IReadOnlyCollection<Movie>> GetAllMoviesReadOnly(CancellationToken cancellationToken)
        {
            return await _context.Movies.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<Movie>> GetMoviesByIdReadOnly(IEnumerable<string> movieIds, CancellationToken cancellationToken)
        {
            return await _context.Movies.Where(m => movieIds.Contains(m.Id)).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task AddMovie(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
