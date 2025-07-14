using Microsoft.AspNetCore.Mvc;

namespace TrackFlix.Models
{
    public class DeleteItemDto
    {
        public string Type { get; set; }

        public int? MovieId { get; set; }
        public int? ShowId { get; set; }
        public int? SeasonId { get; set; }
    }
}
