using Moq;
using MovieWatchlist.Api.Services;
using MovieWatchlist.ApplicationCore.Interfaces.Clients;
using Xunit;

namespace MovieWatchlist.Api.Tests.Unit.Services
{
    public class Top250InfoServiceTests
    {
        private readonly Top250InfoService _top250InfoService;
        private readonly Mock<ITop250InfoClient> _top250InfoClientMock;

        public Top250InfoServiceTests()
        {
            _top250InfoClientMock = new Mock<ITop250InfoClient>();
            _top250InfoService = new Top250InfoService(_top250InfoClientMock.Object);
        }

        [Fact]
        public async Task GetTop250_WhenSuccessResponseFromClient_ReturnsTop250Movies()
        {
            _top250InfoClientMock.Setup(client => client.GetHtml(It.IsAny<string>())).ReturnsAsync(Top250InfoHtmlString());

            var movies = await _top250InfoService.GetTop250();

            Assert.Equal(250, movies.Count);
        }

        private string Top250InfoHtmlString()
        {
            var htmlString = File.ReadAllText("./Services/Top250Info_2022-09-04.html");
            return htmlString;
        }
    }
}
