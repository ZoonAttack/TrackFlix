using Microsoft.AspNetCore.Identity;

namespace TrackFlix.Data
{
    public class User : IdentityUser
    {
        public ICollection<UserMovie> Movies { get; set; }

        public ICollection<UserShow> Shows { get; set; }
    }
}
