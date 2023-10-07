using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MovieWatchlist.ApplicationCore.Interfaces.Services;
using MovieWatchlist.ApplicationCore.Extensions;

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
        [HttpGet("top250")]
        public async Task<IActionResult> GetTop250Movies()
        {
            var movies = await _moviesService.GetTop250();

            var movieResponses = movies.Select(m => m.MapToResponse());

            return Ok(movies);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery][BindRequired] IEnumerable<string> movieIds)
        {
            var movies = await _moviesService.GetMovies(movieIds);

            var movieResponses = movies.Select(m => m.MapToResponse());

            return Ok(movieResponses);
        }
    }
}