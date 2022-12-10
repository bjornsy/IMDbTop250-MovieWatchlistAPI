using Microsoft.AspNetCore.Mvc;
using MovieWatchlist.Api.Models.Requests;
using MovieWatchlist.Api.Services;
using System.Net.Mime;

namespace MovieWatchlist.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WatchlistsController : ControllerBase
    {
        private readonly IWatchlistsService _watchlistsService;

        public WatchlistsController(IWatchlistsService watchlistsService)
        {
            _watchlistsService = watchlistsService;
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("create")]
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

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("addMovies")]
        [HttpPost]
        public async Task<ActionResult> AddMoviesToWatchlist(AddMoviesToWatchlistRequest addMoviesToWatchlistRequest)
        {
            await _watchlistsService.AddMoviesToWatchlist(addMoviesToWatchlistRequest);

            return NoContent();
        }

        //RemoveMovieFromWatchlist

        //SetMovieAsWatched

        //GetWatchlist
    }
}