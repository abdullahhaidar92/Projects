using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Appointment
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
       
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
