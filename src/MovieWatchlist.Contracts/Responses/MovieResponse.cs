using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Contracts.Responses
{
    public record MovieResponse
    {
        [Key]
        public required string Id { get; init; } = string.Empty;

        public required int? Ranking { get; init; }

        [Required]
        public required string Title { get; init; } = string.Empty;

        [Required]
        public required decimal Rating { get; init; }
    }
}