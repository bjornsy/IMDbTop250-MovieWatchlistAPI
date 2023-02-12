using System.Net;
using WireMock.Logging;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace MovieWatchlist.Api.Tests.Integration
{
    public class Top250InfoServer : IDisposable
    {
        private WireMockServer _server;

        public string Url => _server.Url;

        public void Start()
        {
            _server = WireMockServer.Start();
        }

        public void SetupTop250(HttpStatusCode statusCode, Guid guid)
        {
            _server.Given(Request.Create()
                .WithPath(GetTop250Path())
                .WithParam(GetTop250Param())
                .UsingGet())
                .WithGuid(guid)
                .RespondWith(Response.Create()
                    .WithBody(GeneratedTop250ChartHtml())
                    .WithStatusCode(statusCode));
        }

        public IEnumerable<ILogEntry> LogEntries => _server.LogEntries;

        public void Dispose()
        {
            _server.Stop();
            _server.Dispose();
        }

        private string GetTop250Path()
        {
            return "/charts/";
        }

        private string GetTop250Param()
        {
            return DateTime.Now.ToString("yyyy/MM/dd");
        }

        private string GeneratedTop250ChartHtml()
        {
            var htmlString = File.ReadAllText("./TestData/Top250Info_2022-09-17.html");
            return htmlString;
        }
    }
}
