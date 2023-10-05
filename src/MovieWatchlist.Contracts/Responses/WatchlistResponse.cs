using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Contracts.Responses
{
    public record WatchlistResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public IReadOnlyCollection<MovieInWatchlistResponse> Movies { get; set; } = new List<MovieInWatchlistResponse>();
    }
}
