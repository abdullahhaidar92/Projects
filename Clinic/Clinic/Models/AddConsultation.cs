using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class AddConsultation
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(20)]
        public string Temperature { get; set; }
        [Required]
        [StringLength(20)]
        public string BloodPressure { get; set; }
        [Required]
        public string Symptoms { get; set; }
        [Required]
        public string Diagnosis { get; set; }
        [Required]
        public string Treatment { get; set; }
        [Required]
        [StringLength(20)]
        public string Cost { get; set; }
        [Required]
        public long SelectedPatientId { get; set; }
        public SelectListItem[]  Patients { get; set; }
        public bool InsuranceConfirmation { get; set; } = false;
        [Required]
        public string Type { get; set; }
        public SelectListItem[] Types = new SelectListItem[]{ new SelectListItem { Value ="New Consultaion",Text="New Consultaion"},
                                                                                                       new SelectListItem { Value ="Regular Checkup",Text="Regular Checkup"} ,
                                                                                                        new SelectListItem { Value ="Emergency Situation",Text="Emergency Situation"}};

    }
}
