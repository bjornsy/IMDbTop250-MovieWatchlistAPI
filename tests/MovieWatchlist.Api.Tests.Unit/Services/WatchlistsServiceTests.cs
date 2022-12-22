using Moq;
using MovieWatchlist.Api.Models.Requests;
using MovieWatchlist.Api.Services;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.ApplicationCore.Models;
using Xunit;

namespace MovieWatchlist.Api.Tests.Unit.Services
{
    public class WatchlistsServiceTests
    {
        private Mock<IWatchlistsRepository> _watchlistsRepositoryMock;
        private WatchlistsService _watchlistsService;

        public WatchlistsServiceTests()
        {
            _watchlistsRepositoryMock = new Mock<IWatchlistsRepository>();
            _watchlistsService = new WatchlistsService(_watchlistsRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateWatchlist_GivenPopulatedRequest_ReturnsCreatedWatchlist()
        {
            var createWatchlistRequest = new CreateWatchlistRequest
            {
                Name = "watchlist name",
                MovieIds = new List<string>
                {
                    "movieId"
                }
            };

            var watchlistId = Guid.NewGuid();

            var createdWatchlist = new Watchlist
            {
                Id = watchlistId,
                Name = "watchlist name"
            };

            _watchlistsRepositoryMock.Setup(m => m.SaveWatchlist(It.Is<Watchlist>(w => w.Name.Equals(createWatchlistRequest.Name)), createWatchlistRequest.MovieIds)).ReturnsAsync(createdWatchlist);

            var result = await _watchlistsService.CreateWatchlist(createWatchlistRequest);

            Assert.Equal(createdWatchlist.Id, result.Id);
            Assert.Equal(createWatchlistRequest.Name, result.Name);
        }

        [Fact]
        public async Task CreateWatchlist_GivenNullPropertiesInRequest_ReturnsCreatedWatchlist()
        {
            var defaultName = "Movies to watch from IMDb Top 250";

            var createWatchlistRequest = new CreateWatchlistRequest
            {
            };

            var watchlistId = Guid.NewGuid();

            var createdWatchlist = new Watchlist
            {
                Id = watchlistId,
                Name = defaultName
            };

            _watchlistsRepositoryMock.Setup(m => m.SaveWatchlist(It.Is<Watchlist>(w => w.Name.Equals(createWatchlistRequest.Name)), createWatchlistRequest.MovieIds)).ReturnsAsync(createdWatchlist);

            var result = await _watchlistsService.CreateWatchlist(createWatchlistRequest);

            Assert.Equal(createdWatchlist.Id, result.Id);
            Assert.Equal(defaultName, result.Name);
        }

        [Fact]
        public async Task AddMoviesToWatchlist_ReturnsTask()
        {
            var addMoviesToWatchlistRequest = new AddMoviesToWatchlistRequest
            {
                WatchlistId = Guid.NewGuid(),
                MovieIds = new List<string>()
            };

            _watchlistsRepositoryMock.Setup(m => m.AddMoviesToWatchlist(addMoviesToWatchlistRequest.WatchlistId, addMoviesToWatchlistRequest.MovieIds)).Returns(Task.CompletedTask);

            await _watchlistsService.AddMoviesToWatchlist(addMoviesToWatchlistRequest);
        }

        [Fact]
        public async Task RemoveMoviesFromWatchlist_ReturnsTask()
        {
            var removeMoviesFromWatchlistRequest = new RemoveMoviesFromWatchlistRequest
            {
                WatchlistId = Guid.NewGuid(),
                MovieIds = new List<string>()
            };

            _watchlistsRepositoryMock.Setup(m => m.RemoveMoviesFromWatchlist(removeMoviesFromWatchlistRequest.WatchlistId, removeMoviesFromWatchlistRequest.MovieIds)).Returns(Task.CompletedTask);

            await _watchlistsService.RemoveMoviesFromWatchlist(removeMoviesFromWatchlistRequest);
        }
    }
}
