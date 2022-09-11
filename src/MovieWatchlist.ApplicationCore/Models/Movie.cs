using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWatchlist.ApplicationCore.Models
{
    [Table("Movies", Schema = "Movies")]
    public class Movie
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public int Ranking { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        [Column(TypeName = "numeric(2,1)")]
        public decimal Rating { get; set; }
    }
}