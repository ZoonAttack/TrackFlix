using Netflix_Clone.Data;

namespace Netflix_Clone.Models
{
    public class HomeViewModel
    {
        public List<Movie> TrendingMovies { get; set; }

        public List<Show> TrendingShows { get; set; }
        public List<Movie> RecommendedMovies { get; set; }

        public List<Show> RecommendedShows { get; set; }
    }
}
