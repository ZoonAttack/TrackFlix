using TrackFlix.Data.DTOs;

namespace TrackFlix.Data
{
    public class TMDBResponse<T>
    {
        public int Page { get; set; }
        public List<T> Results { get; set; }
        public int Total_Pages { get; set; }
        public int Total_Results { get; set; }
    }
}
