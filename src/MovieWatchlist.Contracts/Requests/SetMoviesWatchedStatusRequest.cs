using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Contracts.Requests
{
    public class SetMoviesWatchedStatusRequest
    {
        [Required]
        [MinLength(1)]
        public Dictionary<string, bool> MovieIdsWatched { get; set; } = new Dictionary<string, bool>();
    }
}
