using System.ComponentModel.DataAnnotations;

namespace EventAppUI.ViewModels
{
    public class Login
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
