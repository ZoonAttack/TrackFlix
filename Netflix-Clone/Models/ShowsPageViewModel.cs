using Netflix_Clone.Data.Utility;
using Netflix_Clone.Data;

namespace Netflix_Clone.Models
{
    public class ShowsPageViewModel
    {
        public List<Genre> Genres { get; set; }

        public List<Show> Shows { get; set; }


        ////Filtering 
        //public string Genre { get; set; }

        //public string Sort { get; set; }

        //public int Page { get; set; }
    }
}
