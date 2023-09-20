using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWatchlist.ApplicationCore.Models
{
    [Table("WatchlistsMovies", Schema = "Watchlists")]
    public class WatchlistsMovies
    {
        [Required]
        public Guid WatchlistId { get; set; }

        [Required]
        public string MovieId { get; set; } = string.Empty;

        [Required]
        public bool Watched { get; set; }

        [Required]
        public DateTimeOffset Created { get; set; }

        [Required]
        public DateTimeOffset LastUpdated { get; set; }
    }
}
