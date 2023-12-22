using MovieWatchlist.ApplicationCore.Models;
using MovieWatchlist.ApplicationCore.Models.DTO;

namespace MovieWatchlist.ApplicationCore.Mapping
{
    public static class DTOMapping
    {
        public static MovieDTO MapToDTO(this Movie movie)
        {
            return new MovieDTO(movie.Id, movie.Ranking, movie.Title, movie.Rating, movie.Created, movie.LastUpdated);
        }
    }
}
