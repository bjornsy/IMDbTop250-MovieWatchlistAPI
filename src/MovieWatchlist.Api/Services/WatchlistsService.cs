using MovieWatchlist.Api.Models.Requests;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.Api.Services
{
    public interface IWatchlistsService
    {
        Task<Watchlist> CreateWatchlist(CreateWatchlistRequest createWatchlistRequest);
        Task DeleteWatchlist(Guid watchlistId);
        Task AddMoviesToWatchlist(AddMoviesToWatchlistRequest addMoviesToWatchlistRequest);
        Task RemoveMoviesFromWatchlist(RemoveMoviesFromWatchlistRequest addMoviesToWatchlistRequest);
    }

    public class WatchlistsService : IWatchlistsService
    {
        private readonly IWatchlistsRepository _watchlistRepository;

        public WatchlistsService(IWatchlistsRepository watchlistRepository)
        {
            _watchlistRepository = watchlistRepository;
        }

        public async Task<Watchlist> CreateWatchlist(CreateWatchlistRequest request)
        {
            var watchlist = new Watchlist
            {
                Name = request.Name
            };

            var movieIds = request.MovieIds;

            return await _watchlistRepository.SaveWatchlist(watchlist, movieIds);
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
    }
}
