﻿using MovieWatchlist.Api.Extensions;
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
        Task<bool> DeleteWatchlist(Guid watchlistId);
        Task<bool> AddMoviesToWatchlist(AddMoviesToWatchlistRequest addMoviesToWatchlistRequest);
        Task<bool> RemoveMoviesFromWatchlist(RemoveMoviesFromWatchlistRequest addMoviesToWatchlistRequest);
        Task<bool> SetMoviesAsWatched(SetMoviesWatchedStatusRequest setMoviesWatchedStatusRequest);
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

        public async Task<WatchlistResponse?> GetWatchlist(Guid watchlistId)
        {
            var watchlist = await _watchlistRepository.GetWatchlistById(watchlistId);

            return watchlist?.MapToResponse();
        }

        public async Task<bool> DeleteWatchlist(Guid watchlistId)
        {
            var watchlist = await _watchlistRepository.GetWatchlistById(watchlistId);
            if (watchlist is null)
            {
                return false;
            }

            var watchlistsMovies = await _watchlistRepository.GetWatchlistsMoviesByWatchlistId(watchlistId);

            _watchlistRepository.RemoveWatchlistsMovies(watchlistsMovies);

            _watchlistRepository.RemoveWatchlist(watchlist);

            await _watchlistRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddMoviesToWatchlist(AddMoviesToWatchlistRequest addMoviesToWatchlistRequest)
        {
            var watchlist = await _watchlistRepository.GetWatchlistById(addMoviesToWatchlistRequest.WatchlistId);
            if (watchlist is null)
            {
                return false;
            }

            var watchlistsMovies = addMoviesToWatchlistRequest.MovieIds.Select(id => new WatchlistsMovies { WatchlistId = addMoviesToWatchlistRequest.WatchlistId, MovieId = id });
            
            await _watchlistRepository.AddWatchlistsMovies(watchlistsMovies);

            await _watchlistRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveMoviesFromWatchlist(RemoveMoviesFromWatchlistRequest removeMoviesFromWatchlistRequest)
        {
            var watchlist = await _watchlistRepository.GetWatchlistById(removeMoviesFromWatchlistRequest.WatchlistId);
            if (watchlist is null)
            {
                return false;
            }

            var watchlistsMovies = removeMoviesFromWatchlistRequest.MovieIds.Select(id => new WatchlistsMovies { WatchlistId = removeMoviesFromWatchlistRequest.WatchlistId, MovieId = id });

            _watchlistRepository.RemoveWatchlistsMovies(watchlistsMovies);

            await _watchlistRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SetMoviesAsWatched(SetMoviesWatchedStatusRequest setMoviesWatchedStatusRequest)
        {
            var watchlistsMoviesByWatchlistId = await _watchlistRepository.GetWatchlistsMoviesByWatchlistId(setMoviesWatchedStatusRequest.WatchlistId);

            var watchlistsMovies = watchlistsMoviesByWatchlistId.Where(wm => setMoviesWatchedStatusRequest.MovieIdsWatched.ContainsKey(wm.MovieId));

            if (!watchlistsMovies.Any())
            {
                return false;
            }

            foreach (var watchlistMovie in watchlistsMovies)
            {
                watchlistMovie.Watched = setMoviesWatchedStatusRequest.MovieIdsWatched[watchlistMovie.MovieId];
            }

            await _watchlistRepository.SaveChangesAsync();

            return true;
        }
    }
}
