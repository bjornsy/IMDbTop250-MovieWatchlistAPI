using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Contracts.Responses
{
    public record MovieResponse([Required] string Id, int? Ranking, [Required] string Title, [Required] decimal Rating);
}