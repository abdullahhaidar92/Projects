using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic.Data;
using Clinic.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Clinic.Controllers
{
    public class DoctorsController : ClinicController
    {
        public readonly IHostingEnvironment _environment;
        public DoctorsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHostingEnvironment environment)
             : base(context, userManager)
        {
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

  
        public async Task<IActionResult> Search(SearchDoctor search)
        {
            ViewData["Role"] = GetRole();
            var query = _context.Doctors.ToArray();
            if (search.SearchId != 0)
            {
                search.Doctors = query.Where(d => d.Doctor_Id == search.SearchId).ToArray();
                return View(search);
            }
            if (search.FirstName != null)
                query = query.Where(d => d.FirstName == search.FirstName).ToArray();
            if (search.MiddleName != null)
                query = query.Where(d => d.MiddleName == search.MiddleName).ToArray();
            if (search.LastName != null)
                query = query.Where(d => d.LastName == search.LastName).ToArray();
            if (search.Speciality != null)
                query = query.Where(d => d.Speciality == search.Speciality).ToArray();
            if (search.Order == "desc")
                query = query.OrderByDescending(d => d.FirstName).ToArray();
            else
                query = query.OrderBy(d => d.FirstName).ToArray();
            search.Doctors = query;
            return View(search);
        }
        
        [Authorize(Policy="Assistant")]
        public async Task<IActionResult> AssistantDoctor()
        {
            string Id = _userManager.GetUserId(User);
            try
            {
                Doctor doctor = (_context.Assistants.Include(a => a.Doctor).First(a => a.Id == Id)).Doctor;
                ViewData["Role"] = GetRole();
                return View("/Views/Doctors/Profile.cshtml",doctor);
            }
            catch (Exception)
            {
                return RedirectToRoute("Assistant");
            }

            
        }
       
        public async Task<IActionResult> Profile()
        {
            string id = _userManager.GetUserId(User);
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

  
        public IActionResult Create()
        {
            long dr_id = 1000;
            try
            {
                dr_id = _context.Doctors.Max(d => d.Doctor_Id) + 1;
            }
            catch (Exception)
            {
            }
            ViewData["dr_id"] = dr_id;
            return View();
        }

        
         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterDoctor registerDoctor,IFormFile file)
        {
            string image = "avatar.jpg";
            if (_context.Doctors.Any(d => d.Doctor_Id == registerDoctor.Doctor_Id))
            {
                ViewData["message"] = "Already Taken";
                return View(registerDoctor);
            }
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var filePath = Path.GetTempFileName();
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\images\\" + file.FileName))
                    {
                        image = file.FileName;
                        file.CopyTo(filestream);
                        filestream.Flush();
                    }
                }
                  
                registerDoctor.Image = image;
                    return View("~/Views/Doctors/FinishCreate.cshtml", registerDoctor);
            }
                
            return View(registerDoctor);
        }

        public async Task<IActionResult> FinishCreate(RegisterDoctor registerDoctor)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registerDoctor.Email, Email = registerDoctor.Email };
                var result = await _userManager.CreateAsync(user, registerDoctor.Password);
                IdentityUser X = await _userManager.FindByEmailAsync(registerDoctor.Email);
                await _userManager.AddClaimAsync(X, new Claim(ClaimTypes.Role, "Doctor"));
                Doctor p = new Doctor
                {
                    Id = user.Id,
                    Email= registerDoctor.Email,
                    Doctor_Id = registerDoctor.Doctor_Id,
                    FirstName = registerDoctor.FirstName,
                    MiddleName = registerDoctor.MiddleName,
                    LastName = registerDoctor.LastName,
                    Gender = registerDoctor.Gender,
                    Image = registerDoctor.Image,
                    Phone=registerDoctor.Phone,
                    Mobile=registerDoctor.Mobile,
                    About=registerDoctor.About,
                    Speciality=registerDoctor.Speciality,
                    Time=registerDoctor.Time,
                    DisplayName=registerDoctor.DisplayName,
                    Address=registerDoctor.Address
                };
                _context.Add(p);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Search));
              
            }
            return View(registerDoctor);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            
            if (User.IsInRole("Doctor"))
            {
                id = _userManager.GetUserId(User);
              
            }

            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Doctor doctor,IFormFile file)
        {
            string returnAction = "Search";
            if (User.IsInRole("Doctor"))
            {
                id = _userManager.GetUserId(User);
                returnAction = "Profile";
            }

            if (id != doctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var filePath = Path.GetTempFileName();
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\images\\" + file.FileName))
                    {
                        doctor.Image = file.FileName;
                        file.CopyTo(filestream);
                        filestream.Flush();
                    }
                }
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(returnAction);
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5


        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Doctor doctor = await _context.Doctors.FindAsync(id);
            IdentityUser user = await _userManager.FindByIdAsync(id);
            Appointment[] appointments = _context.Appointment.Where(a => a.Doctor == doctor).ToArray();
            Consultation[] consultations = _context.Consultations.Where(a => a.Doctor == doctor).ToArray();
            long[] consultationIds = consultations.Select(c => c.Id).ToArray();
            Report[] reports = _context.Report.Where(r => consultationIds.Contains(r.Id)).ToArray();
            Doctor_Patient[] doctor_Patients = _context.Doctor_Patients.Where(d => d.Doctor == doctor).ToArray();
            Assistant[] assistants = _context.Assistants.Where(a => a.Doctor == doctor).ToArray();
            if (user != null)
            {
                Reminder[] reminders = _context.Reminders.Where(r => r.User == user).ToArray();
                _context.Reminders.RemoveRange(reminders);
            }

            _context.Report.RemoveRange(reports);
            _context.Consultations.RemoveRange(consultations);
            _context.Appointment.RemoveRange(appointments);
            _context.Doctor_Patients.RemoveRange(doctor_Patients);
            _context.Assistants.RemoveRange(assistants);
            _context.Doctors.Remove(doctor);

            await _context.SaveChangesAsync();
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Search");
        }
    
   

        private bool DoctorExists(string id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }
  
       
    }
}
