using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Api.Models.Responses
{
    public class Watchlist
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
