using TrackFlix.Data;

namespace TrackFlix.Models
{
    public class CollectionItemPageViewModel<T>
    {
        public T CollectionItem { get; set; }

        public List<Tuple<string, float, string>> UsersRating { get; set; }
    }
}
