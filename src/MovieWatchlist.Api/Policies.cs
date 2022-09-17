using Microsoft.Extensions.Caching.Memory;
using MovieWatchlist.Infrastructure.Clients;
using Polly.Caching.Memory;
using Polly.Extensions.Http;
using Polly;

namespace MovieWatchlist.Api
{
    public static class Policies
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(4, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}
