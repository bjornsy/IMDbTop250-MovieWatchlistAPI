using Microsoft.AspNetCore.Mvc;
using MovieWatchlist.Api.Models.Responses;
using MovieWatchlist.Api.Services;

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
        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<MovieResponse>>> GetTop250Movies()
        {
            var movies = await _moviesService.GetTop250();

            return Ok(movies);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("byWatchlistId/{watchlistId:guid}")]
        public async Task<ActionResult<IReadOnlyCollection<MovieInWatchlistResponse>>> GetMoviesByWatchlistId([FromRoute] Guid watchlistId)
        {
            var movies = await _moviesService.GetMoviesByWatchlistId(watchlistId);

            return Ok(movies);
        }
    }
}