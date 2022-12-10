using CsvHelper;
using CsvHelper.Configuration;
using Moq;
using MovieWatchlist.Api.Models.Responses;
using MovieWatchlist.Api.Services;
using MovieWatchlist.ApplicationCore.Interfaces.Clients;
using MovieWatchlist.ApplicationCore.Models;
using System.Globalization;
using Xunit;

namespace MovieWatchlist.Api.Tests.Unit.Services
{
    public class Top250InfoServiceTests
    {
        private readonly Top250InfoService _top250InfoService;
        private readonly Mock<ITop250InfoClient> _top250InfoClientMock;

        public Top250InfoServiceTests()
        {
            _top250InfoClientMock = new Mock<ITop250InfoClient>();
            _top250InfoService = new Top250InfoService(_top250InfoClientMock.Object);
        }

        [Fact]
        public async Task GetTop250_WhenSuccessResponseFromClient_ReturnsTop250Movies()
        {
            _top250InfoClientMock.Setup(client => client.GetHtml(It.IsAny<string>())).ReturnsAsync(Top250InfoHtmlString());

            var movies = await _top250InfoService.GetTop250();

            Assert.Equal(250, movies.Count);
            AssertAllMovies(movies.ToList());
        }

        private string Top250InfoHtmlString()
        {
            var htmlString = File.ReadAllText("./Services/TestData/Top250Info_2022-09-17.html");
            return htmlString;
        }

        private void AssertAllMovies(List<Movie> moviesResult)
        {
            var expectedMovies = LoadExpectedMovies();

            for (int i = 0; i < moviesResult.Count; i++)
            {
                var expectedMovie = expectedMovies[i];
                var actualMovie = moviesResult[i];

                Assert.Equal(expectedMovie.Id, actualMovie.Id);
                Assert.Equal(expectedMovie.Ranking, actualMovie.Ranking);
                Assert.Equal(expectedMovie.Title, actualMovie.Title);
                Assert.Equal(expectedMovie.Rating, actualMovie.Rating);
            }
        }

        private List<Movie> LoadExpectedMovies()
        {
            using (var reader = new StreamReader("./Services/TestData/Top250Movies.csv"))
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
                AutoMap(CultureInfo.InvariantCulture);
            }
        }
    }
}
