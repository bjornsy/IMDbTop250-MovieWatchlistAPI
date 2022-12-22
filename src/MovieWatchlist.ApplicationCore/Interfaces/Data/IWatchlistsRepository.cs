using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Interfaces.Data
{
    public interface IWatchlistsRepository
    {
        Task<Watchlist> SaveWatchlist(Watchlist watchlist, List<string> movieIds);
        Task AddMoviesToWatchlist(Guid watchlistId, List<string> movieIds);
        Task RemoveMoviesFromWatchlist(Guid watchlistId, List<string> movieIds);
    }
}
