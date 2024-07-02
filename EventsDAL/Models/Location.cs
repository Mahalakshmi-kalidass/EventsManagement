using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.Models
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LocationId")]
        public Guid LocationId { get; set; }


        [Required]
        [Column("LocationName")]
        public string LocationName { get; set; }
    }
}
