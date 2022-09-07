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

        [HttpGet(Name = "GetTop250Movies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetTop250Movies()
        {
            var movies = await _moviesService.GetTop250();

            return Ok(movies);
        }
    }
}