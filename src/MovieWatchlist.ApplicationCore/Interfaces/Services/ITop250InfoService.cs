using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Interfaces.Services
{
    public interface ITop250InfoService
    {
        Task<IReadOnlyCollection<Movie>> GetTop250();
    }
}
