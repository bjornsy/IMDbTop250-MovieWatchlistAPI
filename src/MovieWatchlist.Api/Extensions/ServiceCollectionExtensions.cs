using Polly.Extensions.Http;
using Polly;
using Polly.Contrib.WaitAndRetry;
using MovieWatchlist.Api.Configuration;
using Microsoft.Extensions.Options;

namespace MovieWatchlist.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPolicies(
        this IServiceCollection services)
        {
            //Using OptionsBuilder instead of config directly, for validation
            services.AddOptions<PoliciesOptions>()
                .BindConfiguration(PoliciesOptions.Policies)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            var sp = services.BuildServiceProvider();

            var policiesOptions = sp.GetRequiredService<IOptions<PoliciesOptions>>().Value;

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
