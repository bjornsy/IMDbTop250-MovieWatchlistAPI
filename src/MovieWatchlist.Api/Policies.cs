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
