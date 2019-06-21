using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Clinic.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinic.Models
{
    public class AddAppointment
    {

        [Required]
        public DateTime DateTime { get; set; }

        public string SelectPatientId { get; set; }

        public SelectListItem[] Patients { get; set; }

        public AddAppointment()
        {

        }

        public AddAppointment(ApplicationDbContext _context)
        {
            Fill(_context);
        }

        public AddAppointment(string patient ,ApplicationDbContext context)
            {
            Fill(context);
           SelectPatientId =patient;
            }

        public void Fill(ApplicationDbContext _context)
        {
            Patient[] patients = _context.Patients.ToArray();
            Patients = new SelectListItem[patients.Length];
            for (int i = 0; i < patients.Length; i++)
            {
                Patients[i] = new SelectListItem { Value = patients[i].Id, Text = patients[i].DisplayName };
            }
        }
    }
}
