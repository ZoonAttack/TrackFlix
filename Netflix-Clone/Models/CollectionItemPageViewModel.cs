using Netflix_Clone.Data;

namespace Netflix_Clone.Models
{
    public class CollectionItemPageViewModel<T>
    {
        public T CollectionItem { get; set; }

        public List<Tuple<string, float>> UsersRating { get; set; }
    }
}
