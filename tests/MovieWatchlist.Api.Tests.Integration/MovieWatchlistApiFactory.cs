using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using MovieWatchlist.Api.Configuration;
using MovieWatchlist.Application.Interfaces.Clients;
using MovieWatchlist.Infrastructure.Clients;
using MovieWatchlist.Infrastructure.Data;
using System.Net;
using Testcontainers.PostgreSql;
using WireMock.Logging;
using Xunit;

namespace MovieWatchlist.Api.Tests.Integration
{
    public class MovieWatchlistApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder().Build();

        private readonly Top250InfoServer _top250InfoServer = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            });


            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(MovieWatchlistContext));
                services.AddDbContext<MovieWatchlistContext>(options =>
                    options.UseNpgsql(_dbContainer.GetConnectionString()));

                services.AddHttpClient<ITop250InfoClient, Top250InfoClient>(client =>
                {
                    client.BaseAddress = new Uri(_top250InfoServer.Url);
                })
                .AddPolicyHandlerFromRegistry(RetryPolicyOptions.RetryPolicy)
                .AddPolicyHandlerFromRegistry(CircuitBreakerPolicyOptions.CircuitBreakerPolicy);
            });
        }

        public IEnumerable<ILogEntry> GetWireMockLogEntries(Guid guid) => _top250InfoServer.LogEntries.Where(l => l.MappingGuid.Equals(guid));

        public void SetTop250Response(HttpStatusCode statusCode, Guid guid)
        {
            _top250InfoServer.SetupTop250(statusCode, guid);
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            _top250InfoServer.Start();
        }

        public new async Task DisposeAsync()
        {
            _top250InfoServer.Dispose();
            await _dbContainer.DisposeAsync();
        }
    }
}
