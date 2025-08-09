using TrackFlix.Data;

namespace TrackFlix.Models
{
    public class UserListModel<T1, T2>
    {
        public List<T1> ListData { get; set; }
        public List<T2> CollectionData { get; set; }


    }
}
