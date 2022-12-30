namespace MovieWatchlist.Api.Models.Requests
{
    public class SetMoviesWatchedStatusRequest
    {
        public Guid WatchlistId { get; set; }
        public Dictionary<string, bool> MovieIdsWatched { get; set; } = new Dictionary<string, bool>();
    }
}
