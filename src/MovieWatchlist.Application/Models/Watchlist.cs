using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWatchlist.Application.Models
{
    [Table("Watchlists", Schema = "Watchlists")]
    public class Watchlist
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTimeOffset Created { get; set; }

        [Required]
        public DateTimeOffset LastUpdated { get; set; }

        //For join table
        public List<Movie> Movies { get; } = new();
    }
}
