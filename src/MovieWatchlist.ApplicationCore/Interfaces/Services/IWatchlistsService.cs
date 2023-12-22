using MovieWatchlist.ApplicationCore.Models;
using MovieWatchlist.ApplicationCore.Models.DTO;
using MovieWatchlist.Contracts.Requests;

namespace MovieWatchlist.ApplicationCore.Interfaces.Services
{
    public interface IWatchlistsService
    {
        Task<WatchlistWithMoviesWatchedDTO> CreateWatchlist(CreateWatchlistRequest createWatchlistRequest, CancellationToken cancellationToken);
        Task<WatchlistWithMoviesWatchedDTO?> GetWatchlist(Guid watchlistId, CancellationToken cancellationToken);
        Task DeleteWatchlist(Guid watchlistId, CancellationToken cancellationToken);
        Task AddMoviesToWatchlist(Guid watchlistId, AddMoviesToWatchlistRequest addMoviesToWatchlistRequest);
        Task RemoveMoviesFromWatchlist(Guid watchlistId, RemoveMoviesFromWatchlistRequest addMoviesToWatchlistRequest, CancellationToken cancellationToken);
        Task SetMoviesAsWatched(Guid watchlistId, SetMoviesWatchedStatusRequest setMoviesWatchedStatusRequest, CancellationToken cancellationToken);
        Task Rename(Guid watchlistId, string name);
    }
}
