using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Interfaces.Data
{
    public interface IWatchlistsRepository
    {
        Task<Watchlist> AddWatchlist(Watchlist watchlist);
        Task AddWatchlistsMovies(IEnumerable<WatchlistsMovies> watchlistsMovies);
        Task<Watchlist?> GetWatchlistById(Guid watchlistId, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<WatchlistsMovies>> GetWatchlistsMoviesByWatchlistId(Guid watchlistId, CancellationToken cancellationToken);
        void RemoveWatchlistsMovies(IEnumerable<WatchlistsMovies> watchlistsMovies);
        void RemoveWatchlist(Guid watchlistId);
        void RenameWatchlist(Guid watchlistId, string name);
        Task SaveChangesAsync();
    }
}
