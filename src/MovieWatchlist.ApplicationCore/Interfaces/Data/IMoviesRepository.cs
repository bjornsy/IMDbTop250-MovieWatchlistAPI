using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Interfaces.Data
{
    public interface IMoviesRepository
    {
        Task<IReadOnlyCollection<Movie>> GetTop250();
    }
}
