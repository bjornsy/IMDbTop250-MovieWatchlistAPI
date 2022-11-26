using Polly.Extensions.Http;
using Polly;
using Polly.Contrib.WaitAndRetry;

namespace MovieWatchlist.Api
{
    public static class Policies
    {
        public static IAsyncPolicy<HttpResponseMessage> RetryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 4));

        public static IAsyncPolicy<HttpResponseMessage> CircuitBreakerPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

        //public static IAsyncPolicy<HttpResponseMessage> GetCachePolicy(IServiceProvider serviceProvider)
        //{
        //    var memoryCache = serviceProvider.GetService<IMemoryCache>();
        //    var memoryCacheProvider = new MemoryCacheProvider(memoryCache);

        //    var logger = serviceProvider.GetService<ILogger<Top250InfoClient>>();

        //    var cacheKeyStrategy = (Context c) =>
        //    {
        //        return c.OperationKey;
        //    };
        //    var onCacheGet = (Context c, string s) =>
        //    {
        //        logger.LogInformation("Returning html from cache");
        //    };
        //    var onCacheMiss = (Context c, string s) => {
        //        logger.LogInformation("Cache miss");
        //    };
        //    var onCachePut = (Context c, string s) => {
        //        logger.LogInformation("Adding to cache");
        //    };
        //    var onCacheGetError = (Context c, string s, Exception e) => logger.LogError("Error getting html from cache", e);
        //    var onCachePutError = (Context c, string s, Exception e) => logger.LogError("Error adding html to cache", e);

        //    var cachePolicy = Policy.CacheAsync<HttpResponseMessage>(memoryCacheProvider, TimeSpan.FromMinutes(60), cacheKeyStrategy,
        //        onCacheGet, onCacheMiss, onCachePut, onCacheGetError, onCachePutError);

        //    return cachePolicy;
        //}
    }
}
