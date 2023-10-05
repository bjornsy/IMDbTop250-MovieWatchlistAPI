using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Contracts.Requests
{
    public class RemoveMoviesFromWatchlistRequest
    {
        [Required]
        [MinLength(1)]
        public List<string> MovieIds { get; set; } = new List<string>();
    }
}
