using MovieWatchlist.Api.Models.Responses;
using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.Api.Extensions
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

        public static WatchlistResponse MapToResponse(this Watchlist watchlist)
        {
            return new WatchlistResponse
            {
                Id = watchlist.Id,
                Name = watchlist.Name
            };
        }
    }
}
