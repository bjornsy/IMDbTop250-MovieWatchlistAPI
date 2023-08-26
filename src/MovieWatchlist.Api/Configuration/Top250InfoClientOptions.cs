using System.ComponentModel.DataAnnotations;

namespace MovieWatchlist.Api.Configuration
{
    public class Top250InfoClientOptions
    {
        public const string Top250InfoClient = "Top250InfoClient";

        [Required]
        public required string BaseUrl { get; set; }

        public TimeSpan Timeout { get; set; }
    }
}
