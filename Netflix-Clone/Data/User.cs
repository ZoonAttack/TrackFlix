using Microsoft.AspNetCore.Identity;

namespace Netflix_Clone.Data
{
    public class User : IdentityUser
    {
        public ICollection<UserMovie> Movies { get; set; }

        public ICollection<UserShow> Shows { get; set; }
    }
}
