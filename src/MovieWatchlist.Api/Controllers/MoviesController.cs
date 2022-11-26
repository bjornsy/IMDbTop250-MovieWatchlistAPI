using Microsoft.AspNetCore.Mvc;
using MovieWatchlist.Api.Services;
using MovieWatchlist.ApplicationCore.Models;

namespace MovieWatchlist.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(Name = "GetTop250Movies")]
        public async Task<ActionResult<IReadOnlyCollection<Movie>>> GetTop250Movies()
        {
            var movies = await _moviesService.GetTop250();

            return Ok(movies);
        }

        //[Produces("application/json")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[HttpGet]
        //public async Task<ActionResult<IReadOnlyCollection<Movie>>> GetMoviesByWatchlistId(string watchlistId)
        //{
        //    var movies = await _moviesService.Get
        //    return Ok();
        //}
    }
}