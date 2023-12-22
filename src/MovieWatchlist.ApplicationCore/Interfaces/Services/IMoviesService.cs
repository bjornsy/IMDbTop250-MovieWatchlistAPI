using MovieWatchlist.ApplicationCore.Models;
using MovieWatchlist.ApplicationCore.Models.DTO;

namespace MovieWatchlist.ApplicationCore.Interfaces.Services
{
    public interface IMoviesService
    {
        Task<IReadOnlyCollection<MovieDTO>> GetTop250(CancellationToken cancellationToken);
        Task<IReadOnlyCollection<MovieDTO>> GetMovies(IEnumerable<string> movieIds, CancellationToken cancellationToken);
    }
}
