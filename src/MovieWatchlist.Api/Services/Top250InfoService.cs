using HtmlAgilityPack;
using MovieWatchlist.ApplicationCore.Interfaces.Clients;
using MovieWatchlist.ApplicationCore.Models;
using System.Web;

namespace MovieWatchlist.Api.Services
{
    public class Top250InfoService : ITop250InfoService
    {
        private readonly ITop250InfoClient _top250InfoClient;

        public Top250InfoService(ITop250InfoClient top250InfoClient)
        {
            _top250InfoClient = top250InfoClient;
        }

        public async Task<IReadOnlyCollection<Movie>> GetTop250()
        {
            var relativeUrl = GetTop250RelativeUrl();
            var html = await _top250InfoClient.GetHtml(relativeUrl);

            var tableRows = GetTableRows(html);

            var movies = GetMoviesFromTableRows(tableRows);

            return movies;
        }

        private string GetTop250RelativeUrl()
        {
            return "charts/?" + DateTime.Now.ToString("yyyy/MM/dd");
        }

        private IEnumerable<HtmlNode> GetTableRows(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var table = htmlDocument.DocumentNode.SelectNodes("//table").ElementAt(1);
            var rowClassesToInclude = new HashSet<string>() { "row_same", "row_up", "row_down" };
            var tableRows = table.SelectNodes("tr").Where(tr => rowClassesToInclude.Contains(tr.GetClasses().First()));

            return tableRows;
        }

        private List<Movie> GetMoviesFromTableRows(IEnumerable<HtmlNode> tableRows)
        {
            var rankCellIndex = 0;
            var titleCellIndex = 2;
            var ratingCellIndex = 3;
            var indexes = new HashSet<int> { rankCellIndex, titleCellIndex, ratingCellIndex };

            var movies = new List<Movie>();
            foreach (var row in tableRows)
            {
                var cells = row.SelectNodes("td");

                var movieDatas = new List<string>();
                for (var i = 0; i < cells.Count; i++)
                {
                    if (indexes.Contains(i))
                    {
                        var cellData = HttpUtility.HtmlDecode(cells[i].InnerText);
                        movieDatas.Add(cellData);
                    }
                }

                movies.Add(new Movie
                {
                    Ranking = int.Parse(movieDatas[0]),
                    Title = movieDatas[1],
                    Rating = decimal.Parse(movieDatas[2])
                });
            }

            return movies;
        }
    }
}
