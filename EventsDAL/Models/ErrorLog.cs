using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.Models
{
    public class ErrorLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid logId { get; set; }

        public DateTime Timestamp { get; set; }
        public string Errormessage { get; set; }

        public string StatusCode { get; set; }

        public string StackTrace { get; set; }
    }
}
