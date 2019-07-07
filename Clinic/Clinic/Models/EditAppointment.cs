using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Clinic.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinic.Models
{
    public class EditAppointment
    {

        [Required]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public string Time { get; set; }

        public long SelectPatientId { get; set; }

        public SelectListItem[] Patients { get; set; }

        public EditAppointment()
        {

        }

        public EditAppointment(ApplicationDbContext _context)
        {
            Fill(_context);
        }

        public EditAppointment(long patient ,ApplicationDbContext context)
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
                Patients[i] = new SelectListItem { Value = ""+patients[i].Id, Text = patients[i].DisplayName };
            }
        }
    }
}
