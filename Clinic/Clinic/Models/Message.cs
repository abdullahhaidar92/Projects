using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Message
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
       [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public  string Subject { get; set; }
        [Required]
        [StringLength(500)]
        public string Content { get; set; }
        public DateTime DateTime { get; set; }


    }
}
