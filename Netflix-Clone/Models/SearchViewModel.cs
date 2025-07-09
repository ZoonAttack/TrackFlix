using Netflix_Clone.Data;

namespace Netflix_Clone.Models
{
    public class SearchViewModel
    {
        public List<Movie> Movies { get; set; }
        public List<Show> Shows { get; set; }
        public string Query { get; set; }
    }
}
