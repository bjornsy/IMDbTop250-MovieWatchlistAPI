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

            return CreatedAtAction(nameof(GetWatchlist), new { watchlistId = watchlist.Id }, watchlist);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{watchlistId:guid}")]
        public async Task<ActionResult<WatchlistResponse>> GetWatchlist(Guid watchlistId)
        {
            var watchlist = await _watchlistsService.GetWatchlist(watchlistId);

            if (watchlist is null)
            {
                return NotFound();
            }

            return Ok(watchlist);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{watchlistId:guid}")]
        public async Task<ActionResult> DeleteWatchlist(Guid watchlistId)
        {
            var deleted = await _watchlistsService.DeleteWatchlist(watchlistId);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("addMovies")]
        [HttpPost]
        public async Task<ActionResult> AddMoviesToWatchlist(AddMoviesToWatchlistRequest addMoviesToWatchlistRequest)
        {
            //TODO: Return 400 if no movieIds

            var added = await _watchlistsService.AddMoviesToWatchlist(addMoviesToWatchlistRequest);
            if (!added) { 
                return NotFound(); 
            }

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("removeMovies")]
        [HttpPost]
        public async Task<ActionResult> RemoveMoviesFromWatchlist(RemoveMoviesFromWatchlistRequest removeMoviesFromWatchlistRequest)
        {
            //TODO: Return 400 if no movieIds

            var removed = await _watchlistsService.RemoveMoviesFromWatchlist(removeMoviesFromWatchlistRequest);
            if (!removed)
            {
                return NotFound();
            }

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("setMoviesWatchedStatus")]
        [HttpPatch]
        public async Task<ActionResult> SetMoviesWatchedStatus(SetMoviesWatchedStatusRequest setMoviesWatchedStatusRequest)
        {
            //TODO: Return 400 if no movieIds

            var set = await _watchlistsService.SetMoviesAsWatched(setMoviesWatchedStatusRequest);
            if (!set)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}