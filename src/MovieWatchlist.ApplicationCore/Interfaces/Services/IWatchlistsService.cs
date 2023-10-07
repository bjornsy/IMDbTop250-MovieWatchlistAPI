using MovieWatchlist.ApplicationCore.Models;
using MovieWatchlist.Contracts.Requests;

namespace MovieWatchlist.ApplicationCore.Interfaces.Services
{
    public interface IWatchlistsService
    {
        Task<WatchlistWithMoviesWatched> CreateWatchlist(CreateWatchlistRequest createWatchlistRequest);
        Task<WatchlistWithMoviesWatched?> GetWatchlist(Guid watchlistId);
        Task DeleteWatchlist(Guid watchlistId);
        Task AddMoviesToWatchlist(Guid watchlistId, AddMoviesToWatchlistRequest addMoviesToWatchlistRequest);
        Task RemoveMoviesFromWatchlist(Guid watchlistId, RemoveMoviesFromWatchlistRequest addMoviesToWatchlistRequest);
        Task SetMoviesAsWatched(Guid watchlistId, SetMoviesWatchedStatusRequest setMoviesWatchedStatusRequest);
        Task Rename(Guid watchlistId, string name);
    }
}
