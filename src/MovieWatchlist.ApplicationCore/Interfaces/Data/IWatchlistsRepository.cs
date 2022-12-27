using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Interfaces.Data
{
    public interface IWatchlistsRepository
    {
        Task<Watchlist> SaveWatchlist(Watchlist watchlist, List<string> movieIds);
        Task<Watchlist> GetWatchlistById(Guid watchlistId);
        Task DeleteWatchlist(Guid watchlistId);
        Task AddMoviesToWatchlist(Guid watchlistId, List<string> movieIds);
        Task RemoveMoviesFromWatchlist(Guid watchlistId, List<string> movieIds);
        Task SetMoviesAsWatched(Guid watchlistId, List<string> movieIds);
    }
}
