using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace MovieWatchlist.Api.Tests.Integration
{
    internal class Top250InfoServer : IDisposable
    {
        private WireMockServer _server;

        public string Url => _server.Url;

        public void Start()
        {
            _server = WireMockServer.Start();
        }

        public void SetupTop250()
        {
            _server.Given(Request.Create()
                .WithPath(GetTop250RelativeUrl())
                .UsingGet())
                .RespondWith(Response.Create()
                    .WithBody(GeneratedTop250ChartHtml())
                    .WithStatusCode(200));
        }

        public void Dispose()
        {
            _server.Stop();
            _server.Dispose();
        }

        private string GetTop250RelativeUrl()
        {
            return "charts/?" + DateTime.Now.ToString("yyyy/MM/dd");
        }

        private string GeneratedTop250ChartHtml()
        {
            var htmlString = File.ReadAllText("./TestData/Top250Info_2022-09-17.html");
            return htmlString;
        }
    }
}
