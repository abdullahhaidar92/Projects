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
            var query = _context.Doctors.Include(d=>d.User).ToArray();
            if (search.SearchId != 0)
            {
                search.Doctors = query.Where(d => d.Id == search.SearchId).ToArray();
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
                Doctor doctor = (_context.Assistants.Include(a => a.Doctor).ThenInclude(p => p.User).First(a => a.User.Id == Id)).Doctor;
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
            var doctor = await _context.Doctors.Include(p => p.User)
                .FirstOrDefaultAsync(m => m.User.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

  
        public IActionResult Create()
        {
           
            return View();
        }

        
         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterDoctor registerDoctor,IFormFile file)
        {
            string image = "avatar.jpg";
            if (_context.Doctors.Any(d => d.User.UserName == registerDoctor.Username))
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
                var user = new IdentityUser
                {
                    UserName = registerDoctor.Username,
                    Email = registerDoctor.Email,
                    PhoneNumber=registerDoctor.Phone
                };
                var result = await _userManager.CreateAsync(user, registerDoctor.Password);
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Doctor"));
                Doctor p = new Doctor
                {               
                    FirstName = registerDoctor.FirstName,
                    MiddleName = registerDoctor.MiddleName,
                    LastName = registerDoctor.LastName,
                    Gender = registerDoctor.Gender,
                    Image = registerDoctor.Image,
                    Mobile=registerDoctor.Mobile,
                    About=registerDoctor.About,
                    Speciality=registerDoctor.Speciality,
                    Time=registerDoctor.Time,
                    DisplayName=registerDoctor.DisplayName,
                    Address=registerDoctor.Address,
                    User = user,
                };
                _context.Add(p);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Search));
              
            }
            return View(registerDoctor);
        }

        [Authorize(Roles ="Doctor,Admin")]
        public async Task<IActionResult> Edit(long id)
        {
            Doctor doctor=null;
            if (User.IsInRole("Doctor"))
               doctor =_context.Doctors.Include(d=>d.User).Where(d=>d.User.Id == _userManager.GetUserId(User)).Single(); 
           else
                doctor =  _context.Doctors.Include(d => d.User).Single(d=>d.Id==id);

            if (doctor == null)
            {
                return NotFound();
            }
            EditDoctor model = new EditDoctor
            {
                Id=doctor.Id,
                FirstName = doctor.FirstName,
                MiddleName = doctor.MiddleName,
                LastName = doctor.LastName,
                Gender = doctor.Gender,
                Image = doctor.Image,
                Mobile = doctor.Mobile,
                About = doctor.About,
                Speciality = doctor.Speciality,
                Time = doctor.Time,
                DisplayName = doctor.DisplayName,
                Address = doctor.Address,
                Username=doctor.User.UserName,
                Email=doctor.User.Email,
                Phone=doctor.User.PhoneNumber
            };
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, EditDoctor model,IFormFile file)
        {
            string returnAction = "Search";
            if (id != model.Id)
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
                        model.Image = file.FileName;
                        file.CopyTo(filestream);
                        filestream.Flush();
                    }
                }
                Doctor doctor = _context.Doctors.Include(d => d.User).Single(d => d.Id == model.Id);
                try
                {
               
                 doctor.FirstName = model.FirstName;
                doctor.MiddleName = model.MiddleName;
                doctor.LastName = model.LastName;
                doctor.Gender = model.Gender;
                doctor.Image = model.Image;
                doctor.Mobile = model.Mobile;
                doctor.About = model.About;
                doctor.Speciality = model.Speciality;
                doctor.Time = model.Time;
                doctor.DisplayName = model.DisplayName;
                doctor.Address = model.Address;

                    IdentityUser user = doctor.User;
                    var username = await _userManager.GetUserNameAsync(user);
                    if (model.Username != username)
                    {
                        var setUserNameResult = await _userManager.SetEmailAsync(user, model.Username);

                        if (!setUserNameResult.Succeeded)
                        {
                            var userId = await _userManager.GetUserIdAsync(user);
                            throw new InvalidOperationException($"Unexpected error occurred setting username for user with ID '{userId}'.");
                        }
                    }

                    var email = await _userManager.GetEmailAsync(user);
                    if (model.Email != email)
                    {
                        var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);

                        if (!setEmailResult.Succeeded)
                        {
                            var userId = await _userManager.GetUserIdAsync(user);
                            throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                        }
                    }

                    var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
                    if (model.Phone != phoneNumber)
                    {
                        var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.Phone);
                        if (!setPhoneResult.Succeeded)
                        {
                            var userId = await _userManager.GetUserIdAsync(user);
                            throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                        }
                    }
                    doctor.User = user;
                    _context.Doctors.Update(doctor);
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
            return View(model);
        }


        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            Doctor doctor = _context.Doctors.Include(d=>d.User).Single(d=>d.Id==id);
            IdentityUser user =doctor.User;
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


        public async Task<IActionResult> ResetPassword(string id, string OldPassword,string NewPassword,string ConfirmPassword)
        {
            
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (NewPassword == ConfirmPassword)
            {
                await _userManager.ChangePasswordAsync(user, OldPassword, NewPassword);
            }
           
            return RedirectToAction("Search");
        }

        private bool DoctorExists(long id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }
  
       
    }
}
