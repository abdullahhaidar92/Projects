using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Patient
    {
      
        [Required]
        public long Id { get; set; }
    
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(10)]
        public string Gender { get; set; }
  
        [StringLength(150)]
        public string DisplayName { get; set; }

        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(4)]
        public string BloodType { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        [Phone]
        public string Mobile { get; set; }  
        [StringLength(100)]
        public string Image { get; set; }
        public IdentityUser User { get; set; }
        public InsuranceCompany InsuranceCompany { get; set; }
        public List<Doctor_Patient> Doctors { get; set; }
        public List<Report> Reports { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Consultation> Consultations { get; set; }

    }
}
