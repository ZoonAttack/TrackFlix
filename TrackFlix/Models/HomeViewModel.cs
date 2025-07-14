using TrackFlix.Data;

namespace TrackFlix.Models
{
    public class HomeViewModel
    {
        public List<Movie> TrendingMovies { get; set; }

        public List<Show> TrendingShows { get; set; }
        public List<Movie> RecommendedMovies { get; set; }

        public List<Show> RecommendedShows { get; set; }
    }
}
