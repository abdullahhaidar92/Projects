using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Assistant
    {  
        [Required]
        public long Assistant_Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name ="First Name")]
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
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(150)]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
        [Phone]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Image { get; set; }
        [Required]
        [StringLength(450)]
        public string Id { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        public Doctor Doctor { get; set; }
    }
}
