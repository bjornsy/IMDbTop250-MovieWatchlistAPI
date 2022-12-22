using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Interfaces.Data
{
    public interface IMoviesRepository
    {
        Task<IReadOnlyCollection<Movie>> GetTop250();
        Task<IList<Movie>> GetAll();
        Task<IReadOnlyCollection<MovieInWatchlist>> GetMoviesByWatchlistId(Guid watchlistId);
        Task Save();
    }
}
