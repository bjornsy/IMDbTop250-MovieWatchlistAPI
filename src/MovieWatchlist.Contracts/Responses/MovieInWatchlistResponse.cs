namespace MovieWatchlist.Contracts.Responses
{
    public record MovieInWatchlistResponse
    {
        public required MovieResponse Movie { get; init; }
        public required bool Watched { get; init; }
    }
}
