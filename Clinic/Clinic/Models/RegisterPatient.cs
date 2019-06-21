using Clinic.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class RegisterPatient
    {
        [Required]
        public long Patient_Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
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
        [StringLength(150)]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
        [Phone]
        public string Mobile { get; set; }
        [Phone]
            public string Phone { get; set; }
            [StringLength(100)]
            public string Address { get; set; }
            [StringLength(4)]
            public string BloodType { get; set; }
        [Display(Name = "Insurance Company")]
        public InsuranceCompany InsuranceCompany { get; set; }
           public string Image { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
          public string SelectedInsuranceCompanyId { get; set; }

        
   
}
}
