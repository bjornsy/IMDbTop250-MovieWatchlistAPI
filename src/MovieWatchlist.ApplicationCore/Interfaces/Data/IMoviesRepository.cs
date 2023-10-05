using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Interfaces.Data
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
