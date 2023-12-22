namespace MovieWatchlist.ApplicationCore.Models.DTO
{
    public sealed record MovieDTO(string Id, int? Ranking, string Title, decimal Rating, DateTimeOffset Created, DateTimeOffset LastUpdated);
}
