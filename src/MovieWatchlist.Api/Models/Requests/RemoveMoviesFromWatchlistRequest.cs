namespace MovieWatchlist.Api.Models.Requests
{
    public class RemoveMoviesFromWatchlistRequest
    {
        public Guid WatchlistId { get; set; }
        public List<string> MovieIds { get; set; } = new List<string>();
    }
}
