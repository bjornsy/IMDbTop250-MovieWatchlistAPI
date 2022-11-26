using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWatchlist.ApplicationCore.Models
{
    [Table("WatchlistsMovies", Schema = "Watchlists")]
    public class WatchlistsMovies
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Watchlist")]
        public Guid WatchlistId { get; set; }

        [Required]
        [ForeignKey("Movie")]
        public string MovieId { get; set; } = string.Empty;

        [Required]
        public bool Watched { get; set; }
    }
}
