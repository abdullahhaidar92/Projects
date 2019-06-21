using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class SearchConsultaion
    {
        public string Patient { get; set; }
        public string Doctor { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Order { get; set; }
        public Assistant[] Assistants { get; set; }
        public List<SelectListItem> Patients { get; set; }
        public List<SelectListItem> Doctors { get; set; }
        public Consultation[] Consultations { get; set; }
       public void FillPatients(Patient[] patients)
        {
            Patients = new List<SelectListItem>();
            Patients.Add(new SelectListItem { Value = "all", Text = "All" });
            foreach(Patient p in patients)
            {
                if (p.DisplayName == Patient)
                    Patients.Add(new SelectListItem { Value = p.Id, Text = p.DisplayName ,Selected=true});
                else
                    Patients.Add(new SelectListItem { Value = p.Id, Text = p.DisplayName});

            }


        }
        public void FillDoctors(Doctor[] doctors)
        {
            Doctors = new List<SelectListItem>();
            Doctors.Add(new SelectListItem { Value = "all", Text = "All" });
            foreach (Doctor p in doctors)
            {
                if (p.DisplayName == Doctor)
                    Doctors.Add(new SelectListItem { Value = p.Id, Text = p.DisplayName, Selected = true });
                else
                    Doctors.Add(new SelectListItem { Value = p.Id, Text = p.DisplayName });

            }


        }

    }
}
