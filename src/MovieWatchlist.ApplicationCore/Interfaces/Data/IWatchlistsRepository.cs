using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Interfaces.Data
{
    public interface IWatchlistsRepository
    {
        Task<Watchlist> AddWatchlist(Watchlist watchlist);
        Task AddWatchlistsMovies(IEnumerable<WatchlistsMovies> watchlistsMovies);
        Task<Watchlist?> GetWatchlistById(Guid watchlistId);
        Task<IReadOnlyCollection<WatchlistsMovies>> GetWatchlistsMoviesByWatchlistId(Guid watchlistId);
        void RemoveWatchlistsMovies(IEnumerable<WatchlistsMovies> watchlistsMovies);
        void RemoveWatchlist(Guid watchlistId);
        Task SaveChangesAsync();
    }
}
