using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using MovieWatchlist.ApplicationCore.Interfaces.Clients;
using MovieWatchlist.Infrastructure.Clients;
using MovieWatchlist.Infrastructure.Data;
using Xunit;

namespace MovieWatchlist.Api.Tests.Integration
{
    public class MovieWatchlistApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
    {
        private readonly TestcontainerDatabase _dbContainer = new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "moviewatchlist",
                Username = "postgres",
                Password = "postgres"
            })
            .Build();

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
                    options.UseNpgsql(_dbContainer.ConnectionString));

                services.AddHttpClient<ITop250InfoClient, Top250InfoClient>(client =>
                {
                    client.BaseAddress = new Uri(_top250InfoServer.Url);
                })
                .AddPolicyHandler(Policies.RetryPolicy)
                .AddPolicyHandler(Policies.CircuitBreakerPolicy);
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            _top250InfoServer.Start();
            _top250InfoServer.SetupTop250();
        }

        public new async Task DisposeAsync()
        {
            _top250InfoServer.Dispose();
            await _dbContainer.DisposeAsync();
        }
    }
}
