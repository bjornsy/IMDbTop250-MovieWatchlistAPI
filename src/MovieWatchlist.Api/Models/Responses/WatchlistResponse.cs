using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Api.Models.Responses
{
    public record WatchlistResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
