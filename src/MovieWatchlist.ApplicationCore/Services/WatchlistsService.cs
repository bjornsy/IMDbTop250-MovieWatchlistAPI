using MovieWatchlist.ApplicationCore.Extensions;
using MovieWatchlist.Contracts.Requests;
using MovieWatchlist.Contracts.Responses;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;
using MovieWatchlist.ApplicationCore.Exceptions;

namespace MovieWatchlist.ApplicationCore.Services
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

    public class WatchlistsService : IWatchlistsService
    {
        private readonly IWatchlistsRepository _watchlistRepository;
        private readonly IMoviesRepository _moviesRepository;

        public WatchlistsService(IWatchlistsRepository watchlistRepository, IMoviesRepository moviesRepository)
        {
            _watchlistRepository = watchlistRepository;
            _moviesRepository = moviesRepository;
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

            var moviesInWatchlist = await GetMoviesInWatchlist(watchlistsMoviesRecords);

            return createdWatchlist.MapToResponse(moviesInWatchlist)!;
        }

        public async Task<WatchlistResponse?> GetWatchlist(Guid watchlistId)
        {
            var watchlist = await _watchlistRepository.GetWatchlistById(watchlistId);

            if (watchlist == null) { return null; }

            var watchlistsMovies = await _watchlistRepository.GetWatchlistsMoviesByWatchlistId(watchlistId);

            var moviesInWatchlist = await GetMoviesInWatchlist(watchlistsMovies);

            return watchlist.MapToResponse(moviesInWatchlist);
        }

        public async Task DeleteWatchlist(Guid watchlistId)
        {
            var watchlistsMovies = await _watchlistRepository.GetWatchlistsMoviesByWatchlistId(watchlistId);

            _watchlistRepository.RemoveWatchlistsMovies(watchlistsMovies);

            await _watchlistRepository.SaveChangesAsync();

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

        public async Task Rename(Guid watchlistId, string name)
        {
            _watchlistRepository.RenameWatchlist(watchlistId, name);

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

        private async Task<IEnumerable<MovieInWatchlist>> GetMoviesInWatchlist(IEnumerable<WatchlistsMovies> watchlistsMovies)
        {
            var movies = await _moviesRepository.GetMoviesByIdReadOnly(watchlistsMovies.Select(wm => wm.MovieId));

            var moviesInWatchlist = movies.Join(watchlistsMovies, m => m.Id, wm => wm.MovieId, (m, wm) => new { Movie = m, WatchlistsMovies = wm })
                            .Select(x => new MovieInWatchlist(x.Movie, x.WatchlistsMovies.Watched));

            return moviesInWatchlist;
        }
    }
}
