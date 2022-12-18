namespace MovieWatchlist.Api.Models.Responses
{
    public record MovieInWatchlistResponse
    {
        public MovieInWatchlistResponse(MovieResponse movie, bool watched)
        {
            Movie = movie;
            Watched = watched;
        }

        public MovieResponse Movie { get; set; }
        public bool Watched { get; set; }
    }
}
