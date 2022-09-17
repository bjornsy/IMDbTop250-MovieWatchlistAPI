using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.ApplicationCore.Interfaces.Clients
{
    public interface ITop250InfoService
    {
        Task<IReadOnlyCollection<Movie>> GetTop250();
    }
}
