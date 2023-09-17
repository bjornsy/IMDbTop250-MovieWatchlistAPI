﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWatchlist.ApplicationCore.Models
{
    [Table("Watchlists", Schema = "Watchlists")]
    public class Watchlist
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        //For join table
        public List<Movie> Movies { get; } = new();
    }
}
