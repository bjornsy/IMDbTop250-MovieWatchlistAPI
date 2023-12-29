using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MovieWatchlist.ApplicationCore.Exceptions;
using Npgsql;

namespace MovieWatchlist.Api
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;
        private readonly IProblemDetailsService _problemDetailsService;

        private readonly Dictionary<string, string> _knownForeignKeyNamesWithErrorDetail = new()
        {
            ["FK_WatchlistsMovies_Movies_MovieId"] = "Movie ID(s) supplied does not exist in the top 250.",
            ["FK_WatchlistsMovies_Watchlists_WatchlistId"] = "Watchlist for ID supplied does not exist."
        };

        public ExceptionHandler(ILogger<ExceptionHandler> logger, IProblemDetailsService problemDetailsService)
        {
            _logger = logger;
            _problemDetailsService = problemDetailsService;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var exceptionMessage = exception.Message;
            _logger.LogError("Error Message: {exceptionMessage}", exceptionMessage);

            if (exception is DbUpdateException)
            {
                var postgresException = (PostgresException)exception!.InnerException!;
                await Write422ProblemDetailsResponse(httpContext, _knownForeignKeyNamesWithErrorDetail[postgresException.ConstraintName!]);
                return await ValueTask.FromResult(true);
            }

            if (exception is InvalidRequestException)
            {
                await Write422ProblemDetailsResponse(httpContext, exceptionMessage);
                return await ValueTask.FromResult(true);
            }

            return await ValueTask.FromResult(false);
        }

        private async ValueTask Write422ProblemDetailsResponse(HttpContext httpContext, string detail)
        {
            httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

            await _problemDetailsService.WriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails =
                {
                    Title = "Value in request is invalid.",
                    Detail = detail,
                    Type = "https://datatracker.ietf.org/doc/html/rfc4918#section-11.2",
                    Status = 422
                }
            });
        }
    }
}
