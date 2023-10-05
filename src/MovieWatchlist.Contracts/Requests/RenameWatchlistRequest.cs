using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Contracts.Requests
{
    public class RenameWatchlistRequest
    {
        [Required]
        [MinLength(1)]
        public required string Name { get; init; }
    }
}
