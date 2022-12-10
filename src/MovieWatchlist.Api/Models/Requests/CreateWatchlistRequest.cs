namespace MovieWatchlist.Api.Models.Requests
{
    public class CreateWatchlistRequest
    {
        public string Name { get; set; } = "Movies to watch from IMDb Top 250";
        public List<string> MovieIds { get; set; } = new List<string>();
    }
}
