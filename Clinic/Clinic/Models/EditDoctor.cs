using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class EditDoctor
    {
        [Required]
        public long Id { get; set; }


        [Required]
        [StringLength(100)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


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

        [StringLength(10)]
        public string Gender { get; set; }
      
        [Phone]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Speciality { get; set; }  
        [StringLength(100)]
        public string Address { get; set; }
     
        [StringLength(150)]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
        [Phone]
        public string Mobile { get; set; }
        [StringLength(200)]
        public string Time { get; set; }
        [StringLength(1000)]
        public string About { get; set; }

        public string Image { get; set; }





    }
}
