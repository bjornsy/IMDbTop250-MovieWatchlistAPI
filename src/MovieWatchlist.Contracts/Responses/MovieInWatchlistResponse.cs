using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Contracts.Responses
{
    public record MovieInWatchlistResponse([Required] MovieResponse Movie, [Required] bool Watched);
}
