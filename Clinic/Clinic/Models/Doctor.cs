using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Doctor
    {
        [Required]
        public long Id { get; set; }
       
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(10)]
        public string Gender { get; set; }
        [StringLength(150)]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
      
        [Phone]
        public string Mobile { get; set; }
        [StringLength(200)]
        public string Time { get; set; }
        [StringLength(1000)]
        public string About { get; set; }
        [StringLength(100)]
        public string Speciality { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        public string Image { get; set; }
        public IdentityUser User { get; set; }
        public List<Doctor_Patient> Patients { get; set; }
        public List<Report> Reports { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Consultation> Consultations { get; set; }
    
    }
}
