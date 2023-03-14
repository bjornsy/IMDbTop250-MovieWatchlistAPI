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
        [HttpGet("{watchlistId:guid}")]
        public async Task<ActionResult<WatchlistResponse>> GetWatchlist(Guid watchlistId)
        {
            var watchlist = await _watchlistsService.GetWatchlist(watchlistId);

            return Ok(watchlist);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{watchlistId:guid}")]
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
            //TODO: Return 400 if no movieIds

            await _watchlistsService.AddMoviesToWatchlist(addMoviesToWatchlistRequest);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("removeMovies")]
        [HttpPost]
        public async Task<ActionResult> RemoveMoviesFromWatchlist(RemoveMoviesFromWatchlistRequest removeMoviesFromWatchlistRequest)
        {
            //TODO: Return 400 if no movieIds

            await _watchlistsService.RemoveMoviesFromWatchlist(removeMoviesFromWatchlistRequest);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("setMoviesWatchedStatus")]
        [HttpPatch]
        public async Task<ActionResult> SetMoviesWatchedStatus(SetMoviesWatchedStatusRequest setMoviesWatchedStatusRequest)
        {
            //TODO: Return 400 if no movieIds

            await _watchlistsService.SetMoviesAsWatched(setMoviesWatchedStatusRequest);

            return NoContent();
        }
    }
}