using TrackFlix.Data;
using TrackFlix.Data.DTOs;
using TrackFlix.Data.Utility;

namespace TrackFlix.Models
{
    public class MoviesPageViewModel
    {
        public List<Genre> Genres { get; set; }

        public List<Movie> Movies { get; set; }

        //Filtering 
        //public string Genre { get; set; }

        //public string SortBy { get; set; }

        public int Page { get; set; }
    }
}
