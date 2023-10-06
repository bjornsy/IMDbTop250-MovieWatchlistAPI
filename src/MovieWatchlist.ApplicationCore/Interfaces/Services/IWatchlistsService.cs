using MovieWatchlist.Contracts.Requests;
using MovieWatchlist.Contracts.Responses;

namespace MovieWatchlist.ApplicationCore.Interfaces.Services
{
    public interface IWatchlistsService
    {
        Task<WatchlistResponse> CreateWatchlist(CreateWatchlistRequest createWatchlistRequest);
        Task<WatchlistResponse?> GetWatchlist(Guid watchlistId);
        Task DeleteWatchlist(Guid watchlistId);
        Task AddMoviesToWatchlist(Guid watchlistId, AddMoviesToWatchlistRequest addMoviesToWatchlistRequest);
        Task RemoveMoviesFromWatchlist(Guid watchlistId, RemoveMoviesFromWatchlistRequest addMoviesToWatchlistRequest);
        Task SetMoviesAsWatched(Guid watchlistId, SetMoviesWatchedStatusRequest setMoviesWatchedStatusRequest);
        Task Rename(Guid watchlistId, string name);
    }
}
