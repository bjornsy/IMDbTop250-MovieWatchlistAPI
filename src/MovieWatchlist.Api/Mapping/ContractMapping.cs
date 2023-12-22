using MovieWatchlist.Contracts.Responses;
using MovieWatchlist.ApplicationCore.Models;
using MovieWatchlist.ApplicationCore.Models.DTO;

namespace MovieWatchlist.ApplicationCore.Extensions
{
    public static class ContractMapping
    {
        public static MovieResponse MapToResponse(this MovieDTO movie)
        {
            return new MovieResponse
            {
                Id = movie.Id,
                Title = movie.Title,
                Ranking = movie.Ranking,
                Rating = movie.Rating
            };
        }

        public static WatchlistResponse? MapToResponse(this WatchlistWithMoviesWatchedDTO watchlistWithMoviesWatched)
        {
            return new WatchlistResponse
            {
                Id = watchlistWithMoviesWatched.Id,
                Name = watchlistWithMoviesWatched.Name,
                Movies = watchlistWithMoviesWatched.Movies.Select(wm => wm.MapToResponse()).ToList()
            };
        }

        private static MovieInWatchlistResponse MapToResponse(this MovieInWatchlistDTO movieInWatchlist)
        {
            return new MovieInWatchlistResponse
            {
                Movie = movieInWatchlist.MovieDTO.MapToResponse(),
                Watched = movieInWatchlist.Watched
            };
        }
    }
}
