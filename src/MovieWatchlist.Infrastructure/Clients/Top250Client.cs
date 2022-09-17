using MovieWatchlist.ApplicationCore.Interfaces.Clients;

namespace MovieWatchlist.Infrastructure.Clients
{
    public class Top250Client : ITop250Client
    {
        private readonly HttpClient _httpClient; 

        public Top250Client(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetHtml(string relativeUrl)
        {
            return await _httpClient.GetStringAsync(relativeUrl);
        }
    }
}
