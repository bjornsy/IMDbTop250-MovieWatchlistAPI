using CsvHelper;
using CsvHelper.Configuration;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;
using System.Globalization;

namespace MovieWatchlist.Infrastructure.Data
{
    public sealed class DbInitializer : IDbInitializer
    {
        private readonly MovieWatchlistContext _context;

        public DbInitializer(MovieWatchlistContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            SeedTop250Movies();
        }

        private void SeedTop250Movies()
        {
            _context.Database.EnsureCreated();

            if (_context.Movies.Count() < 250)
            {
                _context.RemoveRange(_context.Movies);

                var filepath = Path.GetFullPath("Top250MoviesSeed.csv");
                using (var reader = new StreamReader(filepath))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap<MovieMap>();
                        var movies = csv.GetRecords<Movie>();

                        _context.Movies.AddRange(movies);

                        _context.SaveChanges();
                    }
                }
            }
        }

        private sealed class MovieMap : ClassMap<Movie>
        {
            public MovieMap()
            {
                AutoMap(CultureInfo.InvariantCulture);
            }
        }
    }
}
