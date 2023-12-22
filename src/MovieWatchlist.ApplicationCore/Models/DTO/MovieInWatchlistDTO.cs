namespace MovieWatchlist.ApplicationCore.Models.DTO
{
    public sealed record MovieInWatchlistDTO(MovieDTO MovieDTO, bool Watched);
}
