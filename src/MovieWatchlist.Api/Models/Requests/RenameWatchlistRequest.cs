using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Api.Models.Requests
{
    public class RenameWatchlistRequest
    {
        [Required]
        [MinLength(1)]
        public required string Name { get; set; }
    }
}
