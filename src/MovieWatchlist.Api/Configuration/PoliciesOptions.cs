using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Api.Configuration
{
    public class PoliciesOptions
    {
        public const string Policies = "Policies";

        [Required]
        public required CircuitBreakerPolicyOptions CircuitBreaker { get; set; }

        [Required]
        public required RetryPolicyOptions Retry { get; set; }
    }

    public class CircuitBreakerPolicyOptions
    {
        public const string CircuitBreakerPolicy = "CircuitBreakerPolicy";

        public TimeSpan DurationOfBreak { get; set; }
        public int ExceptionsAllowedBeforeBreaking { get; set; }
    }

    public class RetryPolicyOptions
    {
        public const string RetryPolicy = "RetryPolicy";

        public TimeSpan BackoffMedianFirstRetryDelay { get; set; }

        public int Count { get; set; }
    }
}
