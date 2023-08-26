namespace MovieWatchlist.Api.Configuration
{
    public class Top250InfoClientOptions
    {
        public const string Top250InfoClient = "Top250InfoClient";
        public string BaseUrl { get; set; }
        public TimeSpan Timeout { get; set; }
    }
}
