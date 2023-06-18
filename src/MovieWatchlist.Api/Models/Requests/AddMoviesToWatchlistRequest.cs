using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Api.Models.Requests
{
    public class AddMoviesToWatchlistRequest
    {
        public Guid WatchlistId { get; set; }

        [Required]
        [MinLength(1)]
        public List<string> MovieIds { get; set; } = new List<string>();
    }
}
