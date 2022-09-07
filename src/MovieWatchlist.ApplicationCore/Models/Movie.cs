namespace MovieWatchlist.ApplicationCore.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public int Ranking { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Rating { get; set; }
    }
}