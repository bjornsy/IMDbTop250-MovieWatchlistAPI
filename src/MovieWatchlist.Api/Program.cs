using HealthChecks.UI.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieWatchlist.Api;
using MovieWatchlist.Api.Configuration;
using MovieWatchlist.Api.Extensions;
using MovieWatchlist.ApplicationCore.Services;
using MovieWatchlist.ApplicationCore.Interfaces.Clients;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.Infrastructure.Clients;
using MovieWatchlist.Infrastructure.Data;
using MovieWatchlist.ApplicationCore.Interfaces.Services;
using Asp.Versioning;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Logging.AddConsole();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ExceptionHandler>();

var dbConnectionString = config.GetConnectionString("Postgres") ?? throw new InvalidOperationException("Connection string 'Postgres' not found.");
builder.Services.AddDbContext<MovieWatchlistContext>(options => options.UseNpgsql(dbConnectionString));

var redisConnectionString = config.GetConnectionString("Redis") ?? throw new InvalidOperationException("Connection string 'Redis' not found.");
builder.Services.AddOutputCache(options => { 
    options.DefaultExpirationTimeSpan = TimeSpan.FromHours(1);
    options.AddPolicy("NoCacheIfTest", policyBuilder => { 
        if (builder.Environment.EnvironmentName.Equals("Test")) { policyBuilder.NoCache(); }; 
    });
}).AddStackExchangeRedisOutputCache(options =>
{
    options.ConnectionMultiplexerFactory = async () =>
        await ConnectionMultiplexer.ConnectAsync(redisConnectionString);
});

builder.Services.AddHealthChecks().AddNpgSql(dbConnectionString).AddRedis(redisConnectionString);
builder.Services.AddControllers();
builder.Services.AddApiVersioning(o => {
    o.DefaultApiVersion = new ApiVersion(1.0);
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true;
    o.ApiVersionReader = new MediaTypeApiVersionReader("api-version");
}).AddMvc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();
builder.Services.AddTransient<IMoviesService, MoviesService>();
builder.Services.AddTransient<ITop250InfoService, Top250InfoService>();
builder.Services.AddTransient<ITop250MoviesDatabaseUpdateService, Top250MoviesDatabaseUpdateService>();

builder.Services.AddScoped<IWatchlistsRepository, WatchlistsRepository>();
builder.Services.AddTransient<IWatchlistsService, WatchlistsService>();

builder.Services.AddPolicies();

builder.Services.AddOptions<Top250InfoClientOptions>()
    .BindConfiguration(Top250InfoClientOptions.Top250InfoClient)
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddHttpClient<ITop250InfoClient, Top250InfoClient>((serviceProvider, client) => {
        var httpClientOptions = serviceProvider.GetRequiredService<IOptions<Top250InfoClientOptions>>().Value;
        client.BaseAddress = new Uri(httpClientOptions.BaseUrl);
        client.Timeout = httpClientOptions.Timeout;
    })
    .AddPolicyHandlerFromRegistry(RetryPolicyOptions.RetryPolicy)
    .AddPolicyHandlerFromRegistry(CircuitBreakerPolicyOptions.CircuitBreakerPolicy);

builder.Services.AddHttpLogging(o => { });

var app = builder.Build();

app.UseHttpLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseOutputCache();

app.UseExceptionHandler();

app.MapHealthChecks("/_health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseAuthorization();

app.MapControllers();

app.Run();
