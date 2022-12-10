namespace MovieWatchlist.ApplicationCore.Models
{
    public class AddMoviesToWatchlistRequest
    {
        public Guid WatchlistId { get; set; }
        public List<string> MovieIds { get; set; } = new List<string>();
    }
}
