namespace MovieWatchlist.ApplicationCore.Models.DTO
{
    public sealed record WatchlistWithMoviesWatchedDTO(Guid Id, string Name, IEnumerable<MovieInWatchlistDTO> Movies);
}
