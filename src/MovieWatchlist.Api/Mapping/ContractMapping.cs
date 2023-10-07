using MovieWatchlist.Contracts.Responses;
using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Extensions
{
    public static class ContractMapping
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

        public static WatchlistResponse? MapToResponse(this WatchlistWithMoviesWatched watchlistWithMoviesWatched)
        {
            return new WatchlistResponse
            {
                Id = watchlistWithMoviesWatched.Id,
                Name = watchlistWithMoviesWatched.Name,
                Movies = watchlistWithMoviesWatched.Movies.Select(wm => wm.MapToResponse()).ToList()
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
