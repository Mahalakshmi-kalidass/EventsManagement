

using System.ComponentModel.DataAnnotations;

namespace EventsDAL.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }    

        public string UserName { get; set; }

        
        public string Email {  get; set; }
        public string? Password { get; set; }

        public Role? UserRole { get; set; }

        public Guid EventId { get; set; }
    }

    public enum Role
    {
       Admin, Owner, EventManager, Viewer , EventViewer
    }
}
