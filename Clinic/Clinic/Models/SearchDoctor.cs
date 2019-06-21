using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class SearchDoctor
    {
        public long SearchId { get; set; } = 0; 
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Speciality { get; set; }
        public string Order { get; set; }
        public Doctor[] Doctors { get; set; }
       

    }
}
