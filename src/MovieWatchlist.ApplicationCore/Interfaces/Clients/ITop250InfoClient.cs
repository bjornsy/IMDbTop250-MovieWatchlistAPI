namespace MovieWatchlist.ApplicationCore.Interfaces.Clients
{
    public interface ITop250InfoClient
    {
        Task<string> GetHtml(string relativeUrl);
    }
}
