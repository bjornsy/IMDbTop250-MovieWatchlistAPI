namespace MovieWatchlist.ApplicationCore.Models.DTO
{
    public sealed record MovieDTO
    {
        public string Id { get; set; } = string.Empty;

        public int? Ranking { get; set; }

        public string Title { get; set; } = string.Empty;

        public decimal Rating { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset LastUpdated { get; set; }
    }
}
