﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class SearchAppointments
    {
        public long Patient { get; set; }
        public long Doctor { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
        public Appointment[] Appointments { get; set; }
        public List<SelectListItem> Patients { get; set; }
        public List<SelectListItem> Doctors { get; set; }
        public void FillPatients(Patient[] patients)
        {
            Patients = new List<SelectListItem>();
            Patients.Add(new SelectListItem { Value = "0", Text = "All" });
            foreach(Patient p in patients)
            {
                if (p.Id == Patient)
                    Patients.Add(new SelectListItem { Value = ""+p.Id, Text = p.DisplayName ,Selected=true});
                else
                    Patients.Add(new SelectListItem { Value =""+ p.Id, Text = p.DisplayName});

            }
        }


        public void FillDoctors(Doctor[] doctors)
        {
            Doctors = new List<SelectListItem>();
            Doctors.Add(new SelectListItem { Value = "0", Text = "All" });
            foreach (Doctor p in doctors)
            {
                if (p.Id == Doctor)
                    Doctors.Add(new SelectListItem { Value = ""+p.Id, Text = p.DisplayName, Selected = true });
                else
                    Doctors.Add(new SelectListItem { Value =""+ p.Id, Text = p.DisplayName });

            }
        }


    }
}
