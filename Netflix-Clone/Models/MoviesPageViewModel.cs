using Netflix_Clone.Data;
using Netflix_Clone.Data.DTOs;
using Netflix_Clone.Data.Utility;

namespace Netflix_Clone.Models
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
