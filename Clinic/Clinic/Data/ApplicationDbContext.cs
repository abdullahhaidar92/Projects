using System;
using System.Collections.Generic;
using System.Text;
using Clinic.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
               if( this.InsuranceCompanies.Find("none")==null)
            {
                 InsuranceCompany company = new InsuranceCompany
                {
                    Name = "none",
                    Id = "none",
                 Insurance_Id =0,
                 Email ="none",
                 Phone ="none",
                 Address ="none"
                };
                this.InsuranceCompanies.Add(company);
                this.SaveChanges();
            }
           
        }

         public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Assistant> Assistants { get; set; }


         public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor_Patient> Doctor_Patients { get; set; }
        public DbSet<Consultation> Consultations { get; set; }

        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
