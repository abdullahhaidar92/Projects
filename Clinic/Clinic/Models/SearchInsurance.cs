using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class SearchInsurance
    {
        public long SearchId { get; set; } = 0;
        public string Name { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Order { get; set; }
        public InsuranceCompany [] InsuranceCompanies { get; set; }

       

    }
}
