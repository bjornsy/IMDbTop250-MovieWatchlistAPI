using Microsoft.AspNetCore.Mvc;
using MovieWatchlist.Api.Models.Requests;
using MovieWatchlist.Api.Models.Responses;
using MovieWatchlist.Api.Services;
using System.Net.Mime;

namespace MovieWatchlist.Controllers
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class WatchlistsController : ControllerBase
    {
        private readonly IWatchlistsService _watchlistsService;

        public WatchlistsController(IWatchlistsService watchlistsService)
        {
            _watchlistsService = watchlistsService;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult> CreateWatchlist(CreateWatchlistRequest createWatchlistRequest)
        {
            var watchlist = await _watchlistsService.CreateWatchlist(createWatchlistRequest);


            //TODO: Return location in Created response, after GetWatchlist implemented
            return new ObjectResult(watchlist)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<WatchlistResponse>> GetWatchlist(Guid watchlistId)
        {
            var watchlist = await _watchlistsService.GetWatchlist(watchlistId);

            return Ok(watchlist);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete]
        public async Task<ActionResult> DeleteWatchlist(Guid watchlistId)
        {
            await _watchlistsService.DeleteWatchlist(watchlistId);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("addMovies")]
        [HttpPost]
        public async Task<ActionResult> AddMoviesToWatchlist(AddMoviesToWatchlistRequest addMoviesToWatchlistRequest)
        {
            await _watchlistsService.AddMoviesToWatchlist(addMoviesToWatchlistRequest);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("removeMovies")]
        [HttpPost]
        public async Task<ActionResult> RemoveMoviesFromWatchlist(RemoveMoviesFromWatchlistRequest removeMoviesFromWatchlistRequest)
        {
            await _watchlistsService.RemoveMoviesFromWatchlist(removeMoviesFromWatchlistRequest);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("setMoviesWatched")]
        [HttpPatch]
        public async Task<ActionResult> SetMoviesAsWatched(SetMoviesAsWatchedRequest setMoviesAsWatchedRequest)
        {
            await _watchlistsService.SetMoviesAsWatched(setMoviesAsWatchedRequest);

            return NoContent();
        }
    }
}