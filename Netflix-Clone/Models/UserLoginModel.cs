using System.ComponentModel.DataAnnotations;

namespace Netflix_Clone.Models
{
    public class UserLoginModel
    {
        [Required]
        [StringLength(25)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
