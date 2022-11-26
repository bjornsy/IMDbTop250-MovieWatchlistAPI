using Microsoft.EntityFrameworkCore;
using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.Infrastructure.Data
{
    public class MovieWatchlistContext : DbContext
    {
        public MovieWatchlistContext(DbContextOptions<MovieWatchlistContext> options) : base(options)
        { 
        }

        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Watchlist> Watchlists { get; set; } = null!;
        public DbSet<WatchlistsMovies> WatchlistsMovies { get; set; } = null!;
    }
}
