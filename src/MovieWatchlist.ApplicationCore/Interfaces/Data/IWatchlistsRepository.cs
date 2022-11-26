using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Interfaces.Data
{
    public interface IWatchlistsRepository
    {
        Task<Watchlist> SaveWatchlist(Watchlist watchlist, List<string> movieIds);
    }
}
