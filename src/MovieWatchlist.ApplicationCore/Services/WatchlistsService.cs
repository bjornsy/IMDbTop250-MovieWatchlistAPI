using MovieWatchlist.Contracts.Requests;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;
using MovieWatchlist.ApplicationCore.Exceptions;
using MovieWatchlist.ApplicationCore.Interfaces.Services;
using MovieWatchlist.ApplicationCore.Models.DTO;
using MovieWatchlist.ApplicationCore.Mapping;

namespace MovieWatchlist.ApplicationCore.Services
{
    public class WatchlistsService : IWatchlistsService
    {
        private readonly IWatchlistsRepository _watchlistRepository;
        private readonly IMoviesRepository _moviesRepository;

        public WatchlistsService(IWatchlistsRepository watchlistRepository, IMoviesRepository moviesRepository)
        {
            _watchlistRepository = watchlistRepository;
            _moviesRepository = moviesRepository;
        }

        public async Task<WatchlistWithMoviesWatchedDTO> CreateWatchlist(CreateWatchlistRequest request, CancellationToken cancellationToken)
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

            var moviesInWatchlist = await GetMoviesInWatchlist(watchlistsMoviesRecords, cancellationToken);

            return new WatchlistWithMoviesWatchedDTO(createdWatchlist.Id, createdWatchlist.Name, moviesInWatchlist);
        }

        public async Task<WatchlistWithMoviesWatchedDTO?> GetWatchlist(Guid watchlistId, CancellationToken cancellationToken)
        {
            var watchlist = await _watchlistRepository.GetWatchlistById(watchlistId, cancellationToken);

            if (watchlist == null) { return null; }

            var watchlistsMovies = await _watchlistRepository.GetWatchlistsMoviesByWatchlistId(watchlistId, cancellationToken);

            var moviesInWatchlist = await GetMoviesInWatchlist(watchlistsMovies, cancellationToken);

            return new WatchlistWithMoviesWatchedDTO(watchlist.Id, watchlist.Name, moviesInWatchlist);
        }

        public async Task DeleteWatchlist(Guid watchlistId, CancellationToken cancellationToken)
        {
            var watchlistsMovies = await _watchlistRepository.GetWatchlistsMoviesByWatchlistId(watchlistId, cancellationToken);

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

        public async Task RemoveMoviesFromWatchlist(Guid watchlistId, RemoveMoviesFromWatchlistRequest removeMoviesFromWatchlistRequest, CancellationToken cancellationToken)
        {
            var watchlistsMovies = await ValidateRequest(watchlistId, removeMoviesFromWatchlistRequest.MovieIds, cancellationToken);

            _watchlistRepository.RemoveWatchlistsMovies(watchlistsMovies);

            await _watchlistRepository.SaveChangesAsync();
        }

        public async Task SetMoviesAsWatched(Guid watchlistId, SetMoviesWatchedStatusRequest setMoviesWatchedStatusRequest, CancellationToken cancellationToken)
        {
            var watchlistsMovies = await ValidateRequest(watchlistId, setMoviesWatchedStatusRequest.MovieIdsWatched.Keys, cancellationToken);

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

        private async Task<IEnumerable<WatchlistsMovies>> ValidateRequest(Guid watchlistId, IEnumerable<string> requestMovieIds, CancellationToken cancellationToken)
        {
            var watchlistsMoviesByWatchlistId = await _watchlistRepository.GetWatchlistsMoviesByWatchlistId(watchlistId, cancellationToken);

            var invalidMovieIds = requestMovieIds.Except(watchlistsMoviesByWatchlistId.Select(wm => wm.MovieId));

            if (invalidMovieIds.Any())
            {
                throw new InvalidRequestException(invalidMovieIds);
            }

            var validWatchlistsMovies = watchlistsMoviesByWatchlistId.Where(wm => requestMovieIds.Contains(wm.MovieId));

            return validWatchlistsMovies;
        }

        private async Task<IEnumerable<MovieInWatchlistDTO>> GetMoviesInWatchlist(IEnumerable<WatchlistsMovies> watchlistsMovies, CancellationToken cancellationToken)
        {
            var movies = await _moviesRepository.GetMoviesByIdReadOnly(watchlistsMovies.Select(wm => wm.MovieId), cancellationToken);

            var moviesInWatchlist = movies.Join(watchlistsMovies, m => m.Id, wm => wm.MovieId, (m, wm) => new { Movie = m, WatchlistsMovies = wm })
                            .Select(x => new MovieInWatchlistDTO(x.Movie.MapToDTO(), x.WatchlistsMovies.Watched));

            return moviesInWatchlist;
        }
    }
}
