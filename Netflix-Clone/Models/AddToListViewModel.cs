using TrackFlix.Data;

namespace TrackFlix.Models
{
    public class AddToListViewModel
    {
        public Movie Movie { get; set; }
        public Show Show { get; set; }
        public AddToListModel FormModel { get; set; }
    }
}
