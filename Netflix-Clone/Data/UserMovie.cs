using Netflix_Clone.Data;
using Netflix_Clone.Data.Utility;

public class UserMovie
{
    public string UserId { get; set; }
    public User User { get; set; }

    public int MovieId { get; set; }
    public WatchStatus Status { get; set; }

    public DateTime AddedOn { get; set; }

    public float Rating { get; set; }

}
