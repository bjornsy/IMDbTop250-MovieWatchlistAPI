using Microsoft.EntityFrameworkCore;
using MovieWatchlist.Api;
using MovieWatchlist.Api.Services;
using MovieWatchlist.ApplicationCore.Interfaces.Clients;
using MovieWatchlist.ApplicationCore.Interfaces.Data;
using MovieWatchlist.Infrastructure.Clients;
using MovieWatchlist.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<MovieWatchlistContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MovieWatchlist") ?? throw new InvalidOperationException("Connection string 'MovieWatchlistContext' not found.")));

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();
builder.Services.AddTransient<IMoviesService, MoviesService>();
builder.Services.AddTransient<ITop250InfoService, Top250InfoService>();
builder.Services.AddTransient<ITop250MoviesDatabaseUpdateService, Top250MoviesDatabaseUpdateService>();

builder.Services.AddHttpClient<ITop250InfoClient, Top250InfoClient>(client => client.BaseAddress = new Uri(builder.Configuration["Top250Info:BaseUrl"]))
    .AddPolicyHandler(Policies.GetRetryPolicy())
    .AddPolicyHandler(Policies.GetCircuitBreakerPolicy());

var app = builder.Build();

var logger = app.Logger;

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<MovieWatchlistContext>();
    dataContext.Database.Migrate();

    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
