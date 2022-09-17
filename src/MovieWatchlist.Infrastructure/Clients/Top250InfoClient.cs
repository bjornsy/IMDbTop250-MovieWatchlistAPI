using MovieWatchlist.ApplicationCore.Interfaces.Clients;

namespace MovieWatchlist.Infrastructure.Clients
{
    public class Top250InfoClient : ITop250InfoClient
    {
        private readonly HttpClient _httpClient; 

        public Top250InfoClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetHtml(string relativeUrl)
        {
            return await _httpClient.GetStringAsync(relativeUrl);
        }
    }
}
