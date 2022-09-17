namespace Top250Scraper
{
    internal class Movie
    {
        public string Id { get; set; } = string.Empty;
        public int Ranking { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Rating { get; set; }
    }
}
