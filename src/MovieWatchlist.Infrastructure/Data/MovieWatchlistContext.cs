using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using MovieWatchlist.Application.Models;
using System.Globalization;

namespace MovieWatchlist.Infrastructure.Data
{
    public class MovieWatchlistContext : DbContext
    {
        public MovieWatchlistContext(DbContextOptions<MovieWatchlistContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Watchlist>(entity => 
            {
                entity.Property(e => e.Created).HasDefaultValueSql("now()");

                entity.HasMany(e => e.Movies)
                    .WithMany(e => e.Watchlists)
                    .UsingEntity<WatchlistsMovies>();
            });

            modelBuilder.Entity<WatchlistsMovies>(entity =>
            {
                entity.Property(e => e.Created).HasDefaultValueSql("now()");

                entity.HasIndex(b => b.WatchlistId)
                    .IncludeProperties(b => b.MovieId);
            });

            modelBuilder.Entity<Movie>(entity => 
            {
                entity.Property(e => e.Created).HasDefaultValueSql("now()");

                entity.HasData(GetMoviesFromSeedCsv());
            }); 
        }

        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Watchlist> Watchlists { get; set; } = null!;
        public DbSet<WatchlistsMovies> WatchlistsMovies { get; set; } = null!;

        private IEnumerable<Movie> GetMoviesFromSeedCsv()
        {
            var filepath = Path.GetFullPath("Top250MoviesSeed.csv");
            using (var reader = new StreamReader(filepath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<MovieMap>();
                    var movies = csv.GetRecords<Movie>();

                    return movies.ToList();
                }
            }
        }

        private sealed class MovieMap : ClassMap<Movie>
        {
            public MovieMap()
            {
                Map(m => m.Id).Name("Id");
                Map(m => m.Ranking).Name("Ranking");
                Map(m => m.Title).Name("Title");
                Map(m => m.Rating).Name("Rating");
            }
        }
    }
}
