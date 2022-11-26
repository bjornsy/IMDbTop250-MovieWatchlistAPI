using Microsoft.AspNetCore.Mvc;
using MovieWatchlist.Api.Services;
using MovieWatchlist.ApplicationCore.Models;
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
        [HttpPost(Name = "create")]
        public async Task<ActionResult> CreateWatchlist(CreateWatchlistRequest createWatchlistRequest)
        {
            var watchlist = await _watchlistsService.CreateWatchlist(createWatchlistRequest);

            return new ObjectResult(watchlist)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        //AddMovieToWatchlist

        //RemoveMovieFromWatchlist

        //SetMovieAsWatched
    }
}