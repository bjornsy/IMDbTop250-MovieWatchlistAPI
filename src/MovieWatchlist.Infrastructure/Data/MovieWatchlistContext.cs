using Microsoft.EntityFrameworkCore;
using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.Infrastructure.Data
{
    public class MovieWatchlistContext : DbContext
    {
        public MovieWatchlistContext(DbContextOptions<MovieWatchlistContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Watchlist>()
                .HasMany(e => e.Movies)
                .WithMany(e => e.Watchlists)
                .UsingEntity<WatchlistsMovies>();

            modelBuilder.Entity<WatchlistsMovies>()
                .HasIndex(b => b.WatchlistId)
                .IncludeProperties(b => b.MovieId);
        }

        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Watchlist> Watchlists { get; set; } = null!;
        public DbSet<WatchlistsMovies> WatchlistsMovies { get; set; } = null!;
    }
}
