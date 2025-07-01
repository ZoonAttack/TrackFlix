using Netflix_Clone.Data.DTOs;

namespace Netflix_Clone.Data
{
    public class TMDBResponse<T>
    {
        public int Page { get; set; }
        public List<T> Results { get; set; }
        public int Total_Pages { get; set; }
        public int Total_Results { get; set; }
    }
}
