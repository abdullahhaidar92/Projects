using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Reminder
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(500)]
        public string Content { get; set; }
        [Required]
        public int Priority { get; set; }
        public IdentityUser User { get; set; }
    }
}
