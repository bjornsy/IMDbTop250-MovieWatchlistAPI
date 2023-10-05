using HealthChecks.UI.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieWatchlist.Api;
using MovieWatchlist.Api.Configuration;
using MovieWatchlist.Api.Extensions;
using MovieWatchlist.Api.Services;
using MovieWatchlist.Application.Interfaces.Clients;
using MovieWatchlist.Application.Interfaces.Data;
using MovieWatchlist.Infrastructure.Clients;
using MovieWatchlist.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Logging.AddConsole();

builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks().AddNpgSql(config.GetConnectionString("MovieWatchlist"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

//builder.AddApplication();

builder.Services.AddDbContext<MovieWatchlistContext>(options =>
    options.UseNpgsql(config.GetConnectionString("MovieWatchlist") ?? throw new InvalidOperationException("Connection string 'MovieWatchlistContext' not found.")));

builder.Services.AddTransient<IProblemDetailsWriter, ProblemDetailsWriter>();

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

var app = builder.Build();

app.UseHttpLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        if (context.RequestServices.GetService<IProblemDetailsWriter>() is { } problemDetailsWriter)
        {
            var problemDetailsContext = new ProblemDetailsContext() { HttpContext = context };
            if (problemDetailsWriter.CanWrite(problemDetailsContext))
            {
                await problemDetailsWriter.WriteAsync(problemDetailsContext);
            }
        }
    });
});

app.MapHealthChecks("/_health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseAuthorization();

app.MapControllers();

app.Run();
