using MovieWatchlist.Contracts.Responses;
using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Extensions
{
    public static class ResponseMappingExtensions
    {
        public static MovieResponse MapToResponse(this Movie movie)
        {
            return new MovieResponse
            {
                Id = movie.Id,
                Title = movie.Title,
                Ranking = movie.Ranking,
                Rating = movie.Rating
            };
        }

        public static WatchlistResponse? MapToResponse(this Watchlist? watchlist, IEnumerable<MovieInWatchlist> watchlistsMovies)
        {
            if (watchlist is null)
            {
                return null;
            }

            return new WatchlistResponse
            {
                Id = watchlist.Id,
                Name = watchlist.Name,
                Movies = watchlistsMovies.Select(wm => wm.MapToResponse()).ToList()
            };
        }

        private static MovieInWatchlistResponse MapToResponse(this MovieInWatchlist movieInwatchlist)
        {
            return new MovieInWatchlistResponse
            {
                Movie = movieInwatchlist.Movie.MapToResponse(),
                Watched = movieInwatchlist.Watched
            };
        }
    }
}
