using System.ComponentModel.DataAnnotations;

namespace Netflix_Clone.Models
{
    public class UserRegisterModel
    {
        public string Username { get; set; }
        [Required]
        [StringLength(25)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
