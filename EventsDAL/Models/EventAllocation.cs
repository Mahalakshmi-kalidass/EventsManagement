using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.Models
{
    public class EventAllocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("EventAllocationId")]
        public Guid EventAllocationId { get; set; }

        [Required]
        [Column("EventId")]
        public Guid EventId { get; set; }

        [Required]
        [Column("LocationId")]
        public Guid LocationId {  get; set; }

        [Required]
        [Column("StaffId")]
        public Guid StaffId { get; set; }

       
    }
}
