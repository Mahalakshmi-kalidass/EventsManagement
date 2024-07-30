using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.Models
{
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("StaffId")]
        public Guid StaffId { get; set; }

        [Required]
        [Column("StaffName")]
        public string StaffName { get; set; }

     

        [Column("Email")]
        public string Email { get; set; }

        [Required]
        [Column("LocationId")]
        public Guid LocationId { get; set; }

    }
}
