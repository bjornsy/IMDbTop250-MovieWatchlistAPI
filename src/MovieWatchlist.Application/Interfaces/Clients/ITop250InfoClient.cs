namespace MovieWatchlist.Application.Interfaces.Clients
{
    public interface ITop250InfoClient
    {
        Task<string> GetHtml(string relativeUrl);
    }
}
