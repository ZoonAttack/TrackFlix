using TrackFlix.Data.Utility;

namespace TrackFlix.Data.DTOs
{
    public class TMDBShowDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Original_Language { get; set; }
        public string Overview { get; set; }
        public string Poster_Path { get; set; }
        public string First_Air_Date { get; set; }
        public List<TMDBSeasonDto> Seasons{ get; set; }
        public int Number_Of_Seasons { get; set; }
        public float Vote_Average { get; set; }

        public List<int>? Genre_Ids { get; set; }

        public List<Genre>? Genres { get; set; }
    }
}
