using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Contracts.Responses
{
    public record WatchlistResponse([Required] Guid Id, [Required] string Name, [Required] IEnumerable<MovieInWatchlistResponse> Movies);
}
