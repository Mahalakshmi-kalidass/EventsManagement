using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.Models
{
    public class EventAccess
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Guid EventId { get; set; }    

    }
}
