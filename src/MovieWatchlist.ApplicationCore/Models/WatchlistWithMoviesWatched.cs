namespace MovieWatchlist.ApplicationCore.Models
{
    public record WatchlistWithMoviesWatched
    {
        public required Guid Id { get; init; }

        public required string Name { get; init; } = string.Empty;

        public required IEnumerable<MovieInWatchlist> Movies { get; init; } = Enumerable.Empty<MovieInWatchlist>();
    }
}
