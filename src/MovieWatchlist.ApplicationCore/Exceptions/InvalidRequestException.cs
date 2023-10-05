namespace MovieWatchlist.ApplicationCore.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(IEnumerable<string> invalidMovieIds) : base($"The following movie Ids in the request are invalid: {string.Join(",", invalidMovieIds)}")
        {
            
        }
    }
}
