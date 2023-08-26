using Polly.Extensions.Http;
using Polly;
using Polly.Contrib.WaitAndRetry;
using MovieWatchlist.Api.Configuration;

namespace MovieWatchlist.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPolicies(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            var policiesOptions = configuration.GetRequiredSection(PoliciesOptions.Policies).Get<PoliciesOptions>();

            var policyRegistry = services.AddPolicyRegistry();

            policyRegistry.Add(
                RetryPolicyOptions.RetryPolicy,
                HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(policiesOptions!.Retry.BackoffMedianFirstRetryDelay, policiesOptions.Retry.Count)));
            policyRegistry.Add(
                CircuitBreakerPolicyOptions.CircuitBreakerPolicy,
                HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(policiesOptions.CircuitBreaker.ExceptionsAllowedBeforeBreaking, policiesOptions.CircuitBreaker.DurationOfBreak));

            return services;
        }
    }
}
