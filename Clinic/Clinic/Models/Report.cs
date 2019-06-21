using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Report
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(20)]
        public string Price { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public InsuranceCompany Insurance { get; set; }
        public Consultation Consultation { get; set; }

    }
}
