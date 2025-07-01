namespace Netflix_Clone.Data.DTOs
{
    public class TMDBShowDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Original_Language { get; set; }
        public string Overview { get; set; }
        public string Poster_Path { get; set; }
        public string First_Air_Date { get; set; }
        //public List<Show> Seasons { get; set; }
        public int Number_Of_Seasons { get; set; }
        public float Vote_Average { get; set; }
    }
}
