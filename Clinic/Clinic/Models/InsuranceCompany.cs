using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class InsuranceCompany
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public long Insurance_Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required] 
        [Phone]
        public string Phone { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(100)]
        public string Fax { get; set; }
        [Required]
        [StringLength(450)]
        public string Id { get; set; }
        [StringLength(100)]
        public string Image { get; set; }
        public List<Patient> Patients { get; set; }
        public List<Report> Reports { get; set; }
        
   

    }
}
