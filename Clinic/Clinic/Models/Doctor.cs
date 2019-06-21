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
        public long Doctor_Id { get; set; }
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
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
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
        [Required]
        [StringLength(450)]
        public string Id { get; set; }
        public string Image { get; set; }
        public List<Doctor_Patient> Patients { get; set; }
        public List<Report> Reports { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Consultation> Consultations { get; set; }
    
    }
}
