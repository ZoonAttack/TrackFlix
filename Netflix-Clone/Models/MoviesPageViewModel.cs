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
        public int GenreId { get; set; }

        public string SortBy { get; set; }
    }
}
