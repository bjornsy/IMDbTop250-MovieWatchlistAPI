using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MovieWatchlist.ApplicationCore.Interfaces.Services;
using MovieWatchlist.ApplicationCore.Extensions;
using Asp.Versioning;
using Microsoft.AspNetCore.OutputCaching;

namespace MovieWatchlist.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        [OutputCache]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("top250")]
        public async Task<IActionResult> GetTop250Movies(CancellationToken cancellationToken)
        {
            var movies = await _moviesService.GetTop250(cancellationToken);

            var movieResponses = movies.Select(m => m.MapToResponse());

            return Ok(movieResponses);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery][BindRequired] IEnumerable<string> movieIds, CancellationToken cancellationToken)
        {
            var movies = await _moviesService.GetMovies(movieIds, cancellationToken);

            var movieResponses = movies.Select(m => m.MapToResponse());

            return Ok(movieResponses);
        }
    }
}