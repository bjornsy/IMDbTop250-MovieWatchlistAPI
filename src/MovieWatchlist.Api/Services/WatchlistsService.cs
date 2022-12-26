using MovieWatchlist.Api.Extensions;
using MovieWatchlist.Api.Models.Requests;
using MovieWatchlist.Api.Models.Responses;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.Api.Services
{
    public interface IWatchlistsService
    {
        Task<WatchlistResponse> CreateWatchlist(CreateWatchlistRequest createWatchlistRequest);
        Task<WatchlistResponse> GetWatchlist(Guid watchlistId);
        Task DeleteWatchlist(Guid watchlistId);
        Task AddMoviesToWatchlist(AddMoviesToWatchlistRequest addMoviesToWatchlistRequest);
        Task RemoveMoviesFromWatchlist(RemoveMoviesFromWatchlistRequest addMoviesToWatchlistRequest);
        Task SetMoviesAsWatched(SetMoviesAsWatchedRequest setMoviesAsWatchedRequest);
    }

    public class WatchlistsService : IWatchlistsService
    {
        private readonly IWatchlistsRepository _watchlistRepository;

        public WatchlistsService(IWatchlistsRepository watchlistRepository)
        {
            _watchlistRepository = watchlistRepository;
        }

        public async Task<WatchlistResponse> CreateWatchlist(CreateWatchlistRequest request)
        {
            var watchlist = new Watchlist
            {
                Name = request.Name
            };

            var movieIds = request.MovieIds;

            var createdWatchlist = await _watchlistRepository.SaveWatchlist(watchlist, movieIds);

            return createdWatchlist.MapToResponse();
        }

        public async Task<WatchlistResponse> GetWatchlist(Guid watchlistId)
        {
            var watchlist = await _watchlistRepository.GetWatchlist(watchlistId);

            return watchlist.MapToResponse();
            //TODO: throw 404
        }

        public async Task DeleteWatchlist(Guid watchlistId)
        {
            await _watchlistRepository.DeleteWatchlist(watchlistId);
        }

        public async Task AddMoviesToWatchlist(AddMoviesToWatchlistRequest addMoviesToWatchlistRequest)
        {
            await _watchlistRepository.AddMoviesToWatchlist(addMoviesToWatchlistRequest.WatchlistId, addMoviesToWatchlistRequest.MovieIds);
        }

        public async Task RemoveMoviesFromWatchlist(RemoveMoviesFromWatchlistRequest removeMoviesFromWatchlistRequest)
        {
            await _watchlistRepository.RemoveMoviesFromWatchlist(removeMoviesFromWatchlistRequest.WatchlistId, removeMoviesFromWatchlistRequest.MovieIds);
        }

        public async Task SetMoviesAsWatched(SetMoviesAsWatchedRequest setMoviesAsWatchedRequest)
        {
            await _watchlistRepository.SetMoviesAsWatched(setMoviesAsWatchedRequest.WatchlistId, setMoviesAsWatchedRequest.MovieIds);
        }
    }
}
