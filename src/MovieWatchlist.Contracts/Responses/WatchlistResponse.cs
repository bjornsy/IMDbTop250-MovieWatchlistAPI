using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Contracts.Responses
{
    public record WatchlistResponse
    {
        public required Guid Id { get; init; }

        public required string Name { get; init; } = string.Empty;

        public required IEnumerable<MovieInWatchlistResponse> Movies { get; init; } = Enumerable.Empty<MovieInWatchlistResponse>();
    }
}
