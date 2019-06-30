using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class SearchPatient
    {
        public long SearchId { get; set; } = 0;
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string BloodType { get; set; }
         public DateTime BirthdateFrom { get; set; }
        public DateTime BirthdateTo { get; set; }
        public string Order { get; set; }      
        public bool MyPatients { get; set; }
        public Patient[] Patients { get; set; }
        public long[] EditList { get; set; } = new long[0];


    }
}
