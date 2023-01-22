using MovieWatchlist.Api.Models.Responses;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.Api.Services
{
    public interface ITop250MoviesDatabaseUpdateService
    {
        Task UpdateTop250InDatabase(IReadOnlyCollection<Movie> updatedMovies);
    }

    public class Top250MoviesDatabaseUpdateService : ITop250MoviesDatabaseUpdateService
    {
        private readonly IMoviesRepository _moviesRepository;

        public Top250MoviesDatabaseUpdateService(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public async Task UpdateTop250InDatabase(IReadOnlyCollection<Movie> updatedMovies)
        {
            var dbMovies = await _moviesRepository.GetAllMovies();

            var updatedMoviesDictionary = updatedMovies.ToDictionary(m => m.Id, m => m);

            //Update Ranking and Rating of movies in db
            foreach (var movie in dbMovies)
            {
                var updatedMovieFound = updatedMoviesDictionary.TryGetValue(movie.Id, out var updatedMovie);
                if (updatedMovieFound)
                {
                    movie.Ranking = updatedMovie.Ranking;
                    movie.Rating = updatedMovie.Rating;
                } else
                {
                    //Change ranking of ones not in list to null
                    movie.Ranking = null;
                    //TODO: Keep Rating updated
                }
            }

            //Add new movies
            var dbMovieIds = dbMovies.Select(m => m.Id).ToHashSet();
            var newMoviesIds = updatedMoviesDictionary.Keys.Except(dbMovieIds);

            foreach (var movieId in newMoviesIds)
            {
                await _moviesRepository.AddMovie(updatedMoviesDictionary[movieId]);
            }

            await _moviesRepository.SaveChangesAsync();
        }
    }
}
