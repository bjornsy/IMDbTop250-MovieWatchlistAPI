using CsvHelper.Configuration;
using CsvHelper;
using MovieWatchlist.Application.Models;
using System.Globalization;

namespace MovieWatchlist.Api.Tests.Integration
{
    public interface ISeedMoviesLoader
    {
        IList<Movie> GetSeededTop250Movies();
    }

    internal class SeedMoviesLoader
    {
        public IList<Movie> GetSeededTop250Movies()
        {
            using (var reader = new StreamReader("./TestData/Top250MoviesSeed.csv"))
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
