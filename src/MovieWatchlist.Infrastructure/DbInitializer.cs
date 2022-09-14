using CsvHelper;
using CsvHelper.Configuration;
using MovieWatchlist.ApplicationCore.Models;
using MovieWatchlist.Infrastructure.Data;
using System.Globalization;

namespace MovieWatchlist.Infrastructure
{
    public interface IDbInitializer
    {
        void Initialize();
    }

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

                using (var reader = new StreamReader("../../Top250Scraper/top250Movies.csv"))
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

        public sealed class MovieMap : ClassMap<Movie>
        {
            public MovieMap()
            {
                AutoMap(CultureInfo.InvariantCulture);
                Map(m => m.Id).Ignore();
            }
        }
    }
}
