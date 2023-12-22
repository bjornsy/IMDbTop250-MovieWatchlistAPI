using MovieWatchlist.Contracts.Responses;
using MovieWatchlist.ApplicationCore.Models.DTO;

namespace MovieWatchlist.ApplicationCore.Extensions
{
    public static class ContractMapping
    {
        public static MovieResponse MapToResponse(this MovieDTO movie)
        {
            return new MovieResponse(movie.Id,  movie.Ranking, movie.Title, movie.Rating);
        }

        public static WatchlistResponse? MapToResponse(this WatchlistWithMoviesWatchedDTO watchlistWithMoviesWatched)
        {
            return new WatchlistResponse(watchlistWithMoviesWatched.Id, watchlistWithMoviesWatched.Name, watchlistWithMoviesWatched.Movies.Select(wm => wm.MapToResponse()).ToList());
        }

        private static MovieInWatchlistResponse MapToResponse(this MovieInWatchlistDTO movieInWatchlist)
        {
            return new MovieInWatchlistResponse(movieInWatchlist.MovieDTO.MapToResponse(), movieInWatchlist.Watched);
        }
    }
}
