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

            var createdWatchlist = await _watchlistRepository.AddWatchlist(watchlist);

            var watchlistsMoviesRecords = movieIds.Select(id => new WatchlistsMovies { WatchlistId = createdWatchlist.Id, MovieId = id });
            await _watchlistRepository.AddWatchlistsMovies(watchlistsMoviesRecords);

            await _watchlistRepository.SaveChangesAsync();

            return createdWatchlist.MapToResponse();
        }

        public async Task<WatchlistResponse> GetWatchlist(Guid watchlistId)
        {
            var watchlist = await _watchlistRepository.GetWatchlistById(watchlistId);

            return watchlist.MapToResponse();
            //TODO: throw 404
        }

        public async Task DeleteWatchlist(Guid watchlistId)
        {
            var watchlistsMovies = await _watchlistRepository.GetWatchlistsMoviesByWatchlistId(watchlistId);

            _watchlistRepository.RemoveWatchlistsMovies(watchlistsMovies);

            var watchlist = await _watchlistRepository.GetWatchlistById(watchlistId);

            _watchlistRepository.RemoveWatchlist(watchlist);

            await _watchlistRepository.SaveChangesAsync();
        }

        public async Task AddMoviesToWatchlist(AddMoviesToWatchlistRequest addMoviesToWatchlistRequest)
        {
            var watchlistsMovies = addMoviesToWatchlistRequest.MovieIds.Select(id => new WatchlistsMovies { WatchlistId = addMoviesToWatchlistRequest.WatchlistId, MovieId = id });
            
            await _watchlistRepository.AddWatchlistsMovies(watchlistsMovies);

            await _watchlistRepository.SaveChangesAsync();
        }

        public async Task RemoveMoviesFromWatchlist(RemoveMoviesFromWatchlistRequest removeMoviesFromWatchlistRequest)
        {
            var watchlistsMovies = removeMoviesFromWatchlistRequest.MovieIds.Select(id => new WatchlistsMovies { WatchlistId = removeMoviesFromWatchlistRequest.WatchlistId, MovieId = id });

            _watchlistRepository.RemoveWatchlistsMovies(watchlistsMovies);

            await _watchlistRepository.SaveChangesAsync();
        }

        public async Task SetMoviesAsWatched(SetMoviesAsWatchedRequest setMoviesAsWatchedRequest)
        {
            var watchlistsMoviesByWatchlistId = await _watchlistRepository.GetWatchlistsMoviesByWatchlistId(setMoviesAsWatchedRequest.WatchlistId);

            var watchlistsMovies = watchlistsMoviesByWatchlistId.Where(wm => setMoviesAsWatchedRequest.MovieIds.Contains(wm.MovieId));

            //TODO: Error if none/some not found?

            foreach (var watchlistMovie in watchlistsMovies)
            {
                watchlistMovie.Watched = true;
            }

            await _watchlistRepository.SaveChangesAsync();
        }
    }
}
