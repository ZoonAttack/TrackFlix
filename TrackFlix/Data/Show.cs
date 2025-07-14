using TrackFlix.Data.Utility;

namespace TrackFlix.Data
{
    public class Season
    {
        public string ReleaseDate { get; set; }
        public int Id { get; set; }
        public int EpisodesCount { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PosterURL { get; set; }

        public int SeasonNumber { get; set; }

        public float Rating { get; set; }
    }
        public class Show
        {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public string PosterURL { get; set; }
        public int SeasonsCount { get; set; }
        public List<Season>? Seasons { get; set; }
        public List<string>? Actors { get; set; }
        public string ReleaseDate { get; set; }
        public float Rating { get; set; }

        public ICollection<UserShow> UserShows { get; set; }

        //Might remove later..
        public List<Genre> Genres { get; set; }
        public List<int> GenreIds { get; set; }
    }
}
