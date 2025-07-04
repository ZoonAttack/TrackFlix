using Netflix_Clone.Data;

namespace Netflix_Clone.Models
{
    public class AddToListViewModel
    {
        public Movie Movie { get; set; }
        public Show Show { get; set; }
        public AddToListModel FormModel { get; set; }
    }
}
