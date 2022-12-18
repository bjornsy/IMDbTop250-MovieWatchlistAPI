using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Api.Models.Responses
{
    public record MovieResponse
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        public int? Ranking { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public decimal Rating { get; set; }
    }
}