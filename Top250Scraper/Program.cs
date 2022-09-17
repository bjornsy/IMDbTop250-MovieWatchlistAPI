using CsvHelper;
using HtmlAgilityPack;
using System.Globalization;
using System.Web;
using Top250Scraper;

var top250RelativePath = GetTop250RelativeUrl();

string GetTop250RelativeUrl()
{
    return "charts/?" + DateTime.Now.ToString("yyyy/MM/dd");
}

var httpClient = new HttpClient
{
    BaseAddress = new Uri("http://top250.info/")
};

var html = await httpClient.GetStringAsync(top250RelativePath);

var tableRows = GetTableRows(html);

var movies = GetMoviesFromTableRows(tableRows);

using (var streamWriter = new StreamWriter("../../../../src/MovieWatchlist.Api/Top250MoviesSeed.csv"))
{
    using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
    {
        csvWriter.WriteRecords(movies);
        Console.WriteLine("Saved CSV file");
    }
}




IEnumerable<HtmlNode> GetTableRows(string html)
{
    var htmlDocument = new HtmlDocument();
    htmlDocument.LoadHtml(html);

    var table = htmlDocument.DocumentNode.SelectNodes("//table").ElementAt(1);
    var rowClassesToInclude = new HashSet<string>() { "row_same", "row_up", "row_down" };
    var tableRows = table.SelectNodes("tr").Where(tr => rowClassesToInclude.Contains(tr.GetClasses().First()));

    return tableRows;
}

List<Movie> GetMoviesFromTableRows(IEnumerable<HtmlNode> tableRows)
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

string GetIdFromHref(string href)
{
    var questionMarkIndex = href.LastIndexOf("?");
    var id = href.Substring(questionMarkIndex + 1);

    return id;
}