using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Clinic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


namespace Clinic.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> UserManager;
        public HomeController(UserManager<IdentityUser> userManager)
        {
            
            UserManager = userManager;

        }
        [Authorize]
        public IActionResult Index()
        {
            return RedirectToRoute(GetRole());

        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private string GetRole()
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
