using MovieWatchlist.Contracts.Responses;

namespace MovieWatchlist.ApplicationCore.Interfaces.Services
{
    public interface IMoviesService
    {
        Task<IReadOnlyCollection<MovieResponse>> GetTop250();
        Task<IReadOnlyCollection<MovieResponse>> GetMovies(IEnumerable<string> movieIds);
    }
}
