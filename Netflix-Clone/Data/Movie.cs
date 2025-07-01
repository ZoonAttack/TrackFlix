using Netflix_Clone.Data.Utility;

namespace Netflix_Clone.Data
{
    public class Movie
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public string PosterURL { get; set; }

        public List<string> Actors { get; set; }
        public string ReleaseDate { get; set; }
        public Genre Genre { get; set; }
        public float Rating { get; set; }

        public ICollection<UserMovie> UserMovies { get; set; }
    }
}
