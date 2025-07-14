using TrackFlix.Data;

namespace TrackFlix.Models
{
    public class UserListViewModel
    {
        public UserListModel<UserShow, Show> Shows { get; set; }

        public UserListModel<UserMovie, Movie> Movies { get; set; }
    }
}
