using EventsDAL.Models;
using System.ComponentModel.DataAnnotations;

namespace EventsDAL.ViewModels
{
    public class Register
    {
       
        public string FirstName { get; set; }

        
        public string LastName { get; set; }

        public string Email { get; set; }

       
        public string Password { get; set; }
        
        public string ConfirmPassWord {  get; set; }

      

    }
}
