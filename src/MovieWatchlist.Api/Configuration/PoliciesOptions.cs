namespace MovieWatchlist.Api.Configuration
{
    public class PoliciesOptions
    {
        public const string Policies = "Policies";

        public CircuitBreakerPolicyOptions CircuitBreaker { get; set; }
        public RetryPolicyOptions Retry { get; set; }
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
