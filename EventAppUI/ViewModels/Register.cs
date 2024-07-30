using EventsDAL.Models;
using System.ComponentModel.DataAnnotations;

namespace EventAppUI.ViewModels
{
    public class Register
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage ="First name must be of 2 - 50 length range")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be of 2 - 50 length range")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9]+\.[a-zA-Z]{2,}$",ErrorMessage ="Please enter a valid Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d@#.$%^&*()_+=-]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, and one digit.")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d@#.$%^&*()_+=-]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, and one digit.")]
        
        public string ConfirmPassWord {  get; set; }

     

        
    }
}
