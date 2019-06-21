using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Consultation
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(100)]
        public string Type { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(20)]
        public string Temperature { get; set; }
        [Required]
        [StringLength(20)]
        public string BloodPressure { get; set; }
        [Required]
        public string Symptoms { get; set; }
        [Required]
        public string Diagnosis{ get; set; }
        [Required]
        public string Treatment { get; set; }
        [Required]
        [StringLength(20)]
        public string Cost { get; set; }
        [Required]
        public bool InsuranceConfirmation { get; set; } = false;
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
