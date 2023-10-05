using MovieWatchlist.Application.Models;

namespace MovieWatchlist.Application.Interfaces.Data
{
    public interface IMoviesRepository
    {
        Task<IList<Movie>> GetAllMovies();
        Task<IReadOnlyCollection<Movie>> GetAllMoviesReadOnly();
        Task<IReadOnlyCollection<Movie>> GetMoviesByIdReadOnly(IEnumerable<string> movieIds);
        Task AddMovie(Movie movie);
        Task SaveChangesAsync();
    }
}
