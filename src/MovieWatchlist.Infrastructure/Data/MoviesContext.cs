using Microsoft.EntityFrameworkCore;
using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.Infrastructure.Data
{
    public class MoviesContext : DbContext
    {
        public MoviesContext(DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        public DbSet<Movie>? Movie { get; set; }
    }
}
