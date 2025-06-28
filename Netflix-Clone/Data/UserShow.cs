using Netflix_Clone.Data;
using Netflix_Clone.Data.Utility;


public class UserShow
{
    public string UserId { get; set; }
    public User User { get; set; }

    public int ShowId { get; set; }
    public Show Show { get; set; }

    public WatchStatus Status { get; set; }

    public DateTime AddedOn { get; set; }
}
