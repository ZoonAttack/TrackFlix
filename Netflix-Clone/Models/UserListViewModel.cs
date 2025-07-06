using Netflix_Clone.Data;

namespace Netflix_Clone.Models
{
    public class UserListViewModel
    {
        public UserListModel<UserShow, Show> Shows { get; set; }

        public UserListModel<UserMovie, Movie> Movies { get; set; }
    }
}
