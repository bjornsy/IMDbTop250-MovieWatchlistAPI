using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Contracts.Responses
{
    public record WatchlistResponse
    {
        [Key]
        public required Guid Id { get; init; }

        [Required]
        public required string Name { get; init; } = string.Empty;

        [Required]
        public required IReadOnlyCollection<MovieInWatchlistResponse> Movies { get; init; } = new List<MovieInWatchlistResponse>();
    }
}
