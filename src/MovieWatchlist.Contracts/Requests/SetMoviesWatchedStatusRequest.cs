using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Contracts.Requests
{
    public class SetMoviesWatchedStatusRequest
    {
        [Required]
        [MinLength(1)]
        public required Dictionary<string, bool> MovieIdsWatched { get; init; } = new Dictionary<string, bool>();
    }
}
