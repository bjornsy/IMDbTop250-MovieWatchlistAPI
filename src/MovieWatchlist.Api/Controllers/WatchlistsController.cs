using Microsoft.AspNetCore.Mvc;
using MovieWatchlist.ApplicationCore.Extensions;
using MovieWatchlist.ApplicationCore.Interfaces.Services;
using MovieWatchlist.Contracts.Requests;
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
        public async Task<IActionResult> CreateWatchlist(CreateWatchlistRequest createWatchlistRequest, CancellationToken cancellationToken)
        {
            var watchlistWithMoviesWatched = await _watchlistsService.CreateWatchlist(createWatchlistRequest, cancellationToken);

            var watchlistResponse = watchlistWithMoviesWatched.MapToResponse();

            return CreatedAtAction(nameof(GetWatchlist), new { watchlistId = watchlistResponse!.Id }, watchlistResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{watchlistId}")]
        public async Task<IActionResult> GetWatchlist(Guid watchlistId, CancellationToken cancellationToken)
        {
            var watchlistWithMoviesWatched = await _watchlistsService.GetWatchlist(watchlistId, cancellationToken);

            if (watchlistWithMoviesWatched is null)
            {
                return NotFound();
            }

            var watchlistResponse = watchlistWithMoviesWatched.MapToResponse();

            return Ok(watchlistResponse);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{watchlistId}")]
        public async Task<IActionResult> DeleteWatchlist(Guid watchlistId, CancellationToken cancellationToken)
        {
            var watchlistWithMoviesWatched = await _watchlistsService.GetWatchlist(watchlistId, cancellationToken);
            if (watchlistWithMoviesWatched is null)
            {
                return NotFound();
            }

            await _watchlistsService.DeleteWatchlist(watchlistWithMoviesWatched.Id, cancellationToken);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{watchlistId}/addMovies")]
        [HttpPost]
        public async Task<IActionResult> AddMoviesToWatchlist(Guid watchlistId, AddMoviesToWatchlistRequest addMoviesToWatchlistRequest, CancellationToken cancellationToken)
        {
            var watchlistWithMoviesWatched = await _watchlistsService.GetWatchlist(watchlistId, cancellationToken);
            if (watchlistWithMoviesWatched is null)
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
        public async Task<ActionResult> RemoveMoviesFromWatchlist(Guid watchlistId, RemoveMoviesFromWatchlistRequest removeMoviesFromWatchlistRequest, CancellationToken cancellationToken)
        {
            var watchlistWithMoviesWatched = await _watchlistsService.GetWatchlist(watchlistId, cancellationToken);
            if (watchlistWithMoviesWatched is null)
            {
                return NotFound();
            }

            await _watchlistsService.RemoveMoviesFromWatchlist(watchlistId, removeMoviesFromWatchlistRequest, cancellationToken);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{watchlistId}/setMoviesWatchedStatus")]
        [HttpPatch]
        public async Task<ActionResult> SetMoviesWatchedStatus(Guid watchlistId, SetMoviesWatchedStatusRequest setMoviesWatchedStatusRequest, CancellationToken cancellationToken)
        {
            var watchlistWithMoviesWatched = await _watchlistsService.GetWatchlist(watchlistId, cancellationToken);
            if (watchlistWithMoviesWatched is null)
            {
                return NotFound();
            }

            await _watchlistsService.SetMoviesAsWatched(watchlistId, setMoviesWatchedStatusRequest, cancellationToken);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{watchlistId}/rename")]
        [HttpPatch]
        public async Task<IActionResult> Rename(Guid watchlistId, RenameWatchlistRequest renameWatchlistRequest, CancellationToken cancellationToken)
        {
            var watchlistWithMoviesWatched = await _watchlistsService.GetWatchlist(watchlistId, cancellationToken);
            if (watchlistWithMoviesWatched is null)
            {
                return NotFound();
            }

            await _watchlistsService.Rename(watchlistId, renameWatchlistRequest.Name);

            return NoContent();
        }
    }
}