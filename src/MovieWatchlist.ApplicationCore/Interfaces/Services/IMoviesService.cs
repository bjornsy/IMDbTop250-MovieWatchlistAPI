using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Interfaces.Services
{
    public interface IMoviesService
    {
        Task<IReadOnlyCollection<Movie>> GetTop250();
        Task<IReadOnlyCollection<Movie>> GetMovies(IEnumerable<string> movieIds);
    }
}
