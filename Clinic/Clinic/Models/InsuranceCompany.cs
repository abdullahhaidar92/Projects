using Microsoft.AspNetCore.Identity;
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
        public long Id { get; set; }
   
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(100)]
        public string Fax { get; set; }
        [StringLength(100)]
        public string Image { get; set; }
      
        public IdentityUser User { get; set; }
        public List<Patient> Patients { get; set; }
        public List<Report> Reports { get; set; }
        
   

    }
}
