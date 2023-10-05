using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Contracts.Requests
{
    public class RemoveMoviesFromWatchlistRequest
    {
        [Required]
        [MinLength(1)]
        public required List<string> MovieIds { get; init; } = new List<string>();
    }
}
