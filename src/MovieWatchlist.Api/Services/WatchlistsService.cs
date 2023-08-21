using MovieWatchlist.Api.Exceptions;
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
        Task<WatchlistResponse?> GetWatchlist(Guid watchlistId);
        Task DeleteWatchlist(Guid watchlistId);
        Task AddMoviesToWatchlist(Guid watchlistId, AddMoviesToWatchlistRequest addMoviesToWatchlistRequest);
        Task RemoveMoviesFromWatchlist(Guid watchlistId, RemoveMoviesFromWatchlistRequest addMoviesToWatchlistRequest);
        Task SetMoviesAsWatched(Guid watchlistId, SetMoviesWatchedStatusRequest setMoviesWatchedStatusRequest);
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

            return createdWatchlist.MapToResponse()!;
        }

        public async Task<WatchlistResponse?> GetWatchlist(Guid watchlistId)
        {
            var watchlist = await _watchlistRepository.GetWatchlistById(watchlistId);

            return watchlist?.MapToResponse();
        }

        public async Task DeleteWatchlist(Guid watchlistId)
        {
            var watchlistsMovies = await _watchlistRepository.GetWatchlistsMoviesByWatchlistId(watchlistId);

            _watchlistRepository.RemoveWatchlistsMovies(watchlistsMovies);

            _watchlistRepository.RemoveWatchlist(watchlistId);

            await _watchlistRepository.SaveChangesAsync();
        }

        public async Task AddMoviesToWatchlist(Guid watchlistId, AddMoviesToWatchlistRequest addMoviesToWatchlistRequest)
        {
            var watchlistsMovies = addMoviesToWatchlistRequest.MovieIds.Select(id => new WatchlistsMovies { WatchlistId = watchlistId, MovieId = id });
            
            await _watchlistRepository.AddWatchlistsMovies(watchlistsMovies);

            await _watchlistRepository.SaveChangesAsync();
        }

        public async Task RemoveMoviesFromWatchlist(Guid watchlistId, RemoveMoviesFromWatchlistRequest removeMoviesFromWatchlistRequest)
        {
            var watchlistsMovies = await ValidateRequest(watchlistId, removeMoviesFromWatchlistRequest.MovieIds);

            _watchlistRepository.RemoveWatchlistsMovies(watchlistsMovies);

            await _watchlistRepository.SaveChangesAsync();
        }

        public async Task SetMoviesAsWatched(Guid watchlistId, SetMoviesWatchedStatusRequest setMoviesWatchedStatusRequest)
        {
            var watchlistsMovies = await ValidateRequest(watchlistId, setMoviesWatchedStatusRequest.MovieIdsWatched.Keys);

            foreach (var watchlistMovie in watchlistsMovies)
            {
                watchlistMovie.Watched = setMoviesWatchedStatusRequest.MovieIdsWatched[watchlistMovie.MovieId];
            }

            await _watchlistRepository.SaveChangesAsync();
        }

        private async Task<IEnumerable<WatchlistsMovies>> ValidateRequest(Guid watchlistId, IEnumerable<string> requestMovieIds)
        {
            var watchlistsMoviesByWatchlistId = await _watchlistRepository.GetWatchlistsMoviesByWatchlistId(watchlistId);

            var invalidMovieIds = requestMovieIds.Except(watchlistsMoviesByWatchlistId.Select(wm => wm.MovieId));

            if (invalidMovieIds.Any())
            {
                throw new InvalidRequestException(invalidMovieIds);
            }

            var validWatchlistsMovies = watchlistsMoviesByWatchlistId.Where(wm => requestMovieIds.Contains(wm.MovieId));

            return validWatchlistsMovies;
        }
    }
}
