using HtmlAgilityPack;
using MovieWatchlist.Application.Interfaces.Clients;
using MovieWatchlist.Application.Models;
using System.Web;

namespace MovieWatchlist.Api.Services
{
    public interface ITop250InfoService
    {
        Task<IReadOnlyCollection<Movie>> GetTop250();
    }

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
            var rowClassesToInclude = new HashSet<string>() { "row_same", "row_up", "row_down", "row_new" };
            var tableRows = table.SelectNodes("tr").Where(tr => rowClassesToInclude.Contains(tr.GetClasses().First()));

            return tableRows;
        }

        private List<Movie> GetMoviesFromTableRows(IEnumerable<HtmlNode> tableRows)
        {
            var titleCellIndex = 2;
            var cellIndexToNames = new Dictionary<int, string>
            {
                [0] = "ranking",
                [titleCellIndex] = "title",
                [3] = "rating"
            };

            var movies = new List<Movie>();
            foreach (var row in tableRows)
            {
                var cells = row.SelectNodes("td");

                var movieData = new Dictionary<string, string>();
                for (var i = 0; i < cells.Count; i++)
                {
                    if (cellIndexToNames.Keys.Contains(i))
                    {
                        if (i == titleCellIndex) //Id is within anchor tag of title cell
                        {
                            var anchorHref = cells[i].ChildNodes.Single(n => n.Name.Equals("a")).Attributes["href"].Value;
                            var id = GetIdFromHref(anchorHref);
                            movieData["id"] = id;
                        }
                        var cellData = HttpUtility.HtmlDecode(cells[i].InnerText);
                        movieData[cellIndexToNames[i]] = cellData;
                    }
                }

                movies.Add(new Movie
                {
                    Id = movieData["id"],
                    Ranking = int.Parse(movieData["ranking"]),
                    Title = movieData["title"],
                    Rating = decimal.Parse(movieData["rating"])
                });
            }

            return movies;
        }

        private string GetIdFromHref(string href)
        {
            var questionMarkIndex = href.LastIndexOf("?");
            var id = href.Substring(questionMarkIndex + 1);

            return id;
        }
    }
}
