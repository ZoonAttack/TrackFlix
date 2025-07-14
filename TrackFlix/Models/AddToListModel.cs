using TrackFlix.Data.Utility;

namespace TrackFlix.Models
{
    public class AddToListModel
    {
        public int MovieId { get; set; }
        public int ShowId { get; set; }

        public int SeasonId { get; set; }
        public int EpisodesWatched { get; set; }
        public int UserId { get; set; }
        public float Rating { get; set; }
        public WatchStatus Status { get; set; }
        public string Note { get; set; }
    }
}
