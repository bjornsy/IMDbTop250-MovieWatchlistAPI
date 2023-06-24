using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Api.Models.Requests
{
    public class SetMoviesWatchedStatusRequest
    {
        [Required]
        [MinLength(1)]
        public Dictionary<string, bool> MovieIdsWatched { get; set; } = new Dictionary<string, bool>();
    }
}
