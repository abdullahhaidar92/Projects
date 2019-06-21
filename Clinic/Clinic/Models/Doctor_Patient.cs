using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Doctor_Patient
    {
        [Required]
        public long Id { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
