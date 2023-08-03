using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace MovieWatchlist.Api
{
    public class ForeignKeyConstraintProblemDetailsWriter : IProblemDetailsWriter
    {
        private readonly Dictionary<string, string> _knownForeignKeyNamesWithErrorDetail = new()
        {
            ["FK_WatchlistsMovies_Movies_MovieId"] = "Movie ID(s) supplied does not exist in the top 250.",
            ["FK_WatchlistsMovies_Watchlists_WatchlistId"] = "Watchlist for ID supplied does not exist."
        };

        public bool CanWrite(ProblemDetailsContext context)
        {
            var exceptionHandlerFeature = context.HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exceptionType = exceptionHandlerFeature?.Error;
            if (exceptionType is DbUpdateException)
            {
                var postgresException = exceptionType.InnerException as PostgresException;

                if (postgresException is not null && postgresException.SqlState.Equals(PostgresErrorCodes.ForeignKeyViolation) && _knownForeignKeyNamesWithErrorDetail.Keys.Contains(postgresException.ConstraintName!)) {
                    return true;
                }
            }

            return false;
        }

        public ValueTask WriteAsync(ProblemDetailsContext context)
        {
            var exceptionHandlerFeature = context.HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exceptionType = exceptionHandlerFeature?.Error;
            var postgresException = (PostgresException)exceptionType!.InnerException!;
            var detail = _knownForeignKeyNamesWithErrorDetail[postgresException.ConstraintName!];

            context.ProblemDetails.Title = "Value in request is invalid.";
            context.ProblemDetails.Detail = detail;
            context.ProblemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc4918#section-11.2";
            context.ProblemDetails.Status = 422;

            var response = context.HttpContext.Response;
            response.StatusCode = StatusCodes.Status422UnprocessableEntity;

            return new ValueTask(response.WriteAsJsonAsync(context.ProblemDetails));
        }
    }
}
