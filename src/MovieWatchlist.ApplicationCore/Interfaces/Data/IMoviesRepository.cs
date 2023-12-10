using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Interfaces.Data
{
    public interface IMoviesRepository
    {
        Task<IList<Movie>> GetAllMovies();
        Task<IReadOnlyCollection<Movie>> GetAllMoviesReadOnly(CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Movie>> GetMoviesByIdReadOnly(IEnumerable<string> movieIds, CancellationToken cancellationToken);
        Task AddMovie(Movie movie);
        Task SaveChangesAsync();
    }
}
