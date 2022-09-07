using CsvHelper;
using HtmlAgilityPack;
using System.Globalization;
using System.Web;
using Top250Scraper;

var top250RelativePath = GetTop250RelativePath();

string GetTop250RelativePath()
{
    return "charts/?" + DateTime.Now.ToString("yyyy/MM/dd");
}

var httpClient = new HttpClient
{
    BaseAddress = new Uri("http://top250.info/")
};

var response = await httpClient.GetStringAsync(top250RelativePath);

var htmlDocument = new HtmlDocument();
htmlDocument.LoadHtml(response);

var table = htmlDocument.DocumentNode.SelectNodes("//table").ElementAt(1);
var rowClassesToInclude = new HashSet<string>() { "row_same", "row_up", "row_down" };
var tableRows = table.SelectNodes("tr").Where(tr => rowClassesToInclude.Contains(tr.GetClasses().First()));

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

using (var streamWriter = new StreamWriter("../../../../moviewatchlist-db/top250Movies.csv"))
{
    using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
    {
        csvWriter.WriteRecords(movies);
    }
}