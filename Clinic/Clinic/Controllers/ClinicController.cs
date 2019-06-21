using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic.Data;
using Clinic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Clinic.Controllers
{
    public class ClinicController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected readonly UserManager<IdentityUser> _userManager;

        public ClinicController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            
        }

        public string GetRole()
        {
            if (User.IsInRole("Admin")) return "Admin";
            if (User.IsInRole("Doctor")) return "Doctor";
            if (User.IsInRole("Assistant")) return "Assistant";
            if (User.IsInRole("Patient")) return "Patient";
            if (User.IsInRole("Insurance")) return "Insurance";
            return "";
        }
    }
}