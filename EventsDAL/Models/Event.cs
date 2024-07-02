using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("EventId")]
        public Guid EventId { get; set; }

        [Required]
        [Column("EventName")]
        public string EventName { get; set; }

        [Column("EventDescription")]
        public string EventDescription { get; set; }

        [Required]
        [Column("EventDate")]
        public DateTime EventDate { get; set; }

       
    }
}
