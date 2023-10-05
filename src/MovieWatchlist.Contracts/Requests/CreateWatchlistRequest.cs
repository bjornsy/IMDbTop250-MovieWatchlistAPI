namespace MovieWatchlist.Contracts.Requests
{
    public class CreateWatchlistRequest
    {
        public string Name { get; init; } = "Movies to watch from IMDb Top 250";
        public List<string> MovieIds { get; init; } = new List<string>();
    }
}
