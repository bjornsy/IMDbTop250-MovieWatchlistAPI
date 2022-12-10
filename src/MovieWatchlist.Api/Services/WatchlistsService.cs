using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.Api.Services
{
    public interface IWatchlistsService
    {
        Task<Watchlist> CreateWatchlist(CreateWatchlistRequest createWatchlistRequest);
        Task AddMoviesToWatchlist(AddMoviesToWatchlistRequest addMoviesToWatchlistRequest);
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

        public async Task AddMoviesToWatchlist(AddMoviesToWatchlistRequest addMoviesToWatchlistRequest)
        {
            await _watchlistRepository.AddMoviesToWatchlist(addMoviesToWatchlistRequest.WatchlistId, addMoviesToWatchlistRequest.MovieIds);
        }
    }
}
