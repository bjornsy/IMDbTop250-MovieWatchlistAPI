using Microsoft.AspNetCore.Mvc;
using MovieWatchlist.Contracts.Requests;
using MovieWatchlist.Contracts.Responses;
using MovieWatchlist.ApplicationCore.Services;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult> CreateWatchlist(CreateWatchlistRequest createWatchlistRequest)
        {
            var watchlist = await _watchlistsService.CreateWatchlist(createWatchlistRequest);

            return CreatedAtAction(nameof(GetWatchlist), new { watchlistId = watchlist.Id }, watchlist);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{watchlistId}")]
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{watchlistId}")]
        public async Task<ActionResult> DeleteWatchlist(Guid watchlistId)
        {
            var watchlist = await _watchlistsService.GetWatchlist(watchlistId);
            if (watchlist is null)
            {
                return NotFound();
            }

            await _watchlistsService.DeleteWatchlist(watchlist.Id);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{watchlistId}/addMovies")]
        [HttpPost]
        public async Task<ActionResult> AddMoviesToWatchlist(Guid watchlistId, AddMoviesToWatchlistRequest addMoviesToWatchlistRequest)
        {
            var watchlist = await _watchlistsService.GetWatchlist(watchlistId);
            if (watchlist is null)
            {
                return NotFound();
            }
            
            await _watchlistsService.AddMoviesToWatchlist(watchlistId, addMoviesToWatchlistRequest);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{watchlistId}/removeMovies")]
        [HttpPost]
        public async Task<ActionResult> RemoveMoviesFromWatchlist(Guid watchlistId, RemoveMoviesFromWatchlistRequest removeMoviesFromWatchlistRequest)
        {
            var watchlist = await _watchlistsService.GetWatchlist(watchlistId);
            if (watchlist is null)
            {
                return NotFound();
            }

            await _watchlistsService.RemoveMoviesFromWatchlist(watchlistId, removeMoviesFromWatchlistRequest);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{watchlistId}/setMoviesWatchedStatus")]
        [HttpPatch]
        public async Task<ActionResult> SetMoviesWatchedStatus(Guid watchlistId, SetMoviesWatchedStatusRequest setMoviesWatchedStatusRequest)
        {
            var watchlist = await _watchlistsService.GetWatchlist(watchlistId);
            if (watchlist is null)
            {
                return NotFound();
            }

            await _watchlistsService.SetMoviesAsWatched(watchlistId, setMoviesWatchedStatusRequest);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{watchlistId}/rename")]
        [HttpPatch]
        public async Task<ActionResult> Rename(Guid watchlistId, RenameWatchlistRequest renameWatchlistRequest)
        {
            var watchlist = await _watchlistsService.GetWatchlist(watchlistId);
            if (watchlist is null)
            {
                return NotFound();
            }

            await _watchlistsService.Rename(watchlistId, renameWatchlistRequest.Name);

            return NoContent();
        }
    }
}