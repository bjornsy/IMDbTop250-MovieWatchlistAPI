using MovieWatchlist.ApplicationCore.Models;
using MovieWatchlist.ApplicationCore.Models.DTO;

namespace MovieWatchlist.ApplicationCore.Mapping
{
    public static class DTOMapping
    {
        public static MovieDTO MapToDTO(this Movie movie)
        {
            return new MovieDTO
            {
                Id = movie.Id,
                Ranking = movie.Ranking,
                Title = movie.Title,
                Rating = movie.Rating,
                Created = movie.Created,
                LastUpdated = movie.LastUpdated
            };
        }
    }
}
