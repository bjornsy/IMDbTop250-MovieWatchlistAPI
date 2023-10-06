using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Interfaces.Services
{
    public interface ITop250MoviesDatabaseUpdateService
    {
        Task UpdateTop250InDatabase(IReadOnlyCollection<Movie> updatedMovies);
    }
}
