using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWatchlist.ApplicationCore.Models
{
    [Table("Movies", Schema = "Movies")]
    public class Movie
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        public int? Ranking { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "numeric(2,1)")]
        public decimal Rating { get; set; }

        //For join table
        public List<Watchlist> Watchlists { get; } = new();
    }
}