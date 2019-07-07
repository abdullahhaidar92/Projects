using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class EditAdmin
    {

        [Required]
        public string Id { get; set; }


            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Username")]
        public string Username { get; set; }

      
            [Required]
            [StringLength(50)]
            public string FirstName { get; set; }
            [Required]
            [StringLength(50)]
            public string MiddleName { get; set; }
            [Required]
            [StringLength(50)]
            public string LastName { get; set; }
            [Phone]
            public string Phone { get; set; }
            [Phone]
            public string Mobile { get; set; }

    }
}
