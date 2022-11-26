namespace MovieWatchlist.ApplicationCore.Models
{
    public class MovieInWatchlist
    {
        public MovieInWatchlist(Movie movie, bool watched)
        {
            Movie = movie;
            Watched = watched;
        }

        public Movie Movie { get; set; }
        public bool Watched { get; set; }
    }
}
