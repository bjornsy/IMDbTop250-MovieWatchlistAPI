using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Interfaces.Data
{
    public interface IWatchlistsRepository
    {
        Task<Watchlist> AddWatchlist(Watchlist watchlist);
        Task AddWatchlistsMovies(IEnumerable<WatchlistsMovies> watchlistsMovies);
        Task<Watchlist> GetWatchlistById(Guid watchlistId);
        Task DeleteWatchlist(Guid watchlistId);
        Task AddMoviesToWatchlist(Guid watchlistId, List<string> movieIds);
        Task RemoveMoviesFromWatchlist(Guid watchlistId, List<string> movieIds);
        Task SetMoviesAsWatched(Guid watchlistId, List<string> movieIds);
        Task SaveChangesAsync();
    }
}
