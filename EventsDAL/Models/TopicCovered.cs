using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.Models
{
    public class TopicCovered
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("TopicId")]
        public Guid TopicId { get; set; }

        [Required]
        [Column("TopicName")]
        public string TopicName { get; set; }

        [Required]
        [Column("StaffId")]
        public Guid StaffId { get; set; }

        [Required]
        [Column("EventId")]
        public Guid EventId { get; set; }

        [Required]
        [Column("LocationId")]
        public Guid LocationId { get; set; }
    }
}
