using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Api.Models.Requests
{
    public class RemoveMoviesFromWatchlistRequest
    {
        [Required]
        [MinLength(1)]
        public List<string> MovieIds { get; set; } = new List<string>();
    }
}
