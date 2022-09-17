namespace MovieWatchlist.ApplicationCore.Interfaces.Clients
{
    public interface ITop250Client
    {
        Task<string> GetHtml(string relativeUrl);
    }
}
