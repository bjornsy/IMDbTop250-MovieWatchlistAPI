namespace MovieWatchlist.ApplicationCore.Models.DTO
{
    public sealed record WatchlistDTO(Guid Id, string Name, DateTimeOffset Created, DateTimeOffset LastUpdated);
}
