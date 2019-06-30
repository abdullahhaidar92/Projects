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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Clinic.Controllers
{
    public class PatientsController : ClinicController
    {
        public readonly IHostingEnvironment _environment;
        public PatientsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHostingEnvironment environment)
        :base(context,userManager)
        {
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Authorize(Roles = "Doctor,Admin,Assistant,Insurance")]
        public async Task<IActionResult> Search(SearchPatient search)
        {
            string id = _userManager.GetUserId(User);
            var query = _context.Patients.Include(d => d.User).Include(p => p.InsuranceCompany).ToArray();
            if (User.IsInRole("Doctor"))
            {
                search.EditList = _context.Doctor_Patients.Where(r => r.Doctor.User.Id == id).Select(r => r.Patient.Id).ToArray();
                if (search.MyPatients)
                    query = query.Where(p => search.EditList.Contains(p.Id)).ToArray();      
            }              
            else 
              if (User.IsInRole("Admin")|| User.IsInRole("Assistant"))
                search.EditList = _context.Patients.Select(p => p.Id).ToArray();


            if (search.SearchId != 0)
            {
                search.Patients = query.Where(d => d.Id == search.SearchId).ToArray();
                return View(search);
            }
            if (search.FirstName != null)
                query = query.Where(d => d.FirstName == search.FirstName).ToArray();
            if (search.MiddleName != null)
                query = query.Where(d => d.MiddleName == search.MiddleName).ToArray();
            if (search.LastName != null)
                query = query.Where(d => d.LastName == search.LastName).ToArray();
            if (search.BirthdateFrom != DateTime.MinValue)
                query = query.Where(d => d.Birthdate  >= search.BirthdateFrom).ToArray();
            if (search.BirthdateTo != DateTime.MinValue)
                query = query.Where(d => d.Birthdate <= search.BirthdateTo).ToArray();
            if (search.Order == "desc")
                query = query.OrderByDescending(d => d.FirstName).ToArray();
            else
                query = query.OrderBy(d => d.FirstName).ToArray();
            search.Patients = query;
            return View(search);

        }

        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Profile()
        {
            string id = _userManager.GetUserId(User);
            var patient = await _context.Patients.Include(p=>p.User).Include(p => p.InsuranceCompany)
                .FirstOrDefaultAsync(m => m.User.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [Authorize(Roles = "Admin,Assistant,Doctor")]
        public IActionResult Create()
        {
         
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Assistant,Doctor")]
        public async Task<IActionResult> Create(RegisterPatient registerPatient, IFormFile file)
        {
            string image = "avatar.jpg";
            if (_context.Patients.Any(d => d.User.UserName == registerPatient.Username))
            {
                ViewData["message"] = "Already Taken";
                return View(registerPatient);
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
                    registerPatient.Image = image;
                InsuranceCompany[] IC = _context.InsuranceCompanies.ToArray();
                SelectListItem[] InsuranceCompanies = new SelectListItem[IC.Length + 1];
                InsuranceCompanies[0] = new SelectListItem { Value ="0", Text = "none" };
                for (int i = 0; i < IC.Length; i++)
                {
                    InsuranceCompanies[i + 1] = new SelectListItem { Value = ""+IC[i].Id, Text = IC[i].Name };
                }
                ViewData["InsuranceCompanies"] = InsuranceCompanies;
               
                return View("~/Views/Patients/FinishCreate.cshtml", registerPatient);
            }

            return View(registerPatient);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Assistant,Doctor")]
        public async Task<IActionResult> FinishCreate(RegisterPatient registerPatient)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {  UserName = registerPatient.Username,
                    Email = registerPatient.Email,
                   PhoneNumber= registerPatient.Phone
                };
                var result = await _userManager.CreateAsync(user, registerPatient.Password);
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Patient"));
                InsuranceCompany I = _context.InsuranceCompanies.Find(registerPatient.SelectedInsuranceCompanyId);
                Patient p = new Patient
                {
                    FirstName = registerPatient.FirstName,
                    MiddleName = registerPatient.MiddleName,
                    LastName = registerPatient.LastName,
                    Gender = registerPatient.Gender,
                    Image = registerPatient.Image,
                    Mobile = registerPatient.Mobile,
                    BloodType = registerPatient.BloodType,
                    Birthdate = registerPatient.Birthdate,
                    DisplayName = registerPatient.DisplayName,
                    InsuranceCompany = I,
                    Address = registerPatient.Address,
                    User = user
                };
                if (User.IsInRole("Doctor"))
                {
                    Doctor doctor = _context.Doctors.Find(_userManager.GetUserId(User));
                    Doctor_Patient Relation = new Doctor_Patient()
                    {
                        Doctor = doctor,
                        Patient = p
                    };
                    _context.Add(Relation);
                }

                _context.Add(p);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Search));

            }
            InsuranceCompany[] IC = _context.InsuranceCompanies.ToArray();
            SelectListItem[] InsuranceCompanies = new SelectListItem[IC.Length + 1];
            InsuranceCompanies[0] = new SelectListItem { Value = null, Text = "none" };
            for (int i = 0; i < IC.Length; i++)
            {
                InsuranceCompanies[i + 1] = new SelectListItem { Value = ""+IC[i].Id, Text = IC[i].Name };
            }
            ViewData["InsuranceCompanies"] = InsuranceCompanies;
            return View(registerPatient);
        }

        [Authorize(Roles = "Admin,Assistant,Doctor,Patient")]
        public async Task<IActionResult> Edit(long id)
        {
            Patient patient = null;

            if (User.IsInRole("Doctor") && !_context.Doctor_Patients.Any(r => r.Doctor.User.Id == _userManager.GetUserId(User) && r.Patient.Id == id))
                return View("~/Areas/Identity/Pages/Account/AccessDenied.cshtml");

            if (User.IsInRole("Patient"))
                patient =_context.Patients.Include(p=>p.User).Include(p => p.InsuranceCompany).Where( p=>p.User.Id==_userManager.GetUserId(User)).Single();
            else
                patient = _context.Patients.Include(p => p.User).Include(p => p.InsuranceCompany).Single(p => p.Id == id);

         
            if (patient == null)
            {
                return NotFound();
            }

            ViewData["Companies"] = getCompanies(patient);
            EditPatient model = new EditPatient
            {
                Id=patient.Id,
                FirstName = patient.FirstName,
                MiddleName = patient.MiddleName,
                LastName = patient.LastName,
                Gender = patient.Gender,
                Image = patient.Image,
                Mobile = patient.Mobile,
                BloodType = patient.BloodType,
                Birthdate = patient.Birthdate,
                DisplayName = patient.DisplayName,
                Address = patient.Address,
               Username=patient.User.UserName,
               Email=patient.User.Email,
               Phone=patient.User.PhoneNumber
            };
            return View(model);
        }

        // POST: Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Assistant,Doctor,Patient")]
        public async Task<IActionResult> Edit(long id, EditPatient model,IFormFile file)
        {
            string returnAction = "Search";
            if (id != model.Id)
            {
                return NotFound();
            }

            if (User.IsInRole("Doctor") && !_context.Doctor_Patients.Any(r => r.Doctor.User.Id == _userManager.GetUserId(User) && r.Patient.Id == id))
            {
                return View("~/Areas/Identity/Pages/Account/AccessDenied.cshtml.cs");
            }

            Patient patient = null;
            if (User.IsInRole("Patient"))
            {
                patient = _context.Patients.Include(p => p.User).Include(p => p.InsuranceCompany).Where(p => p.User.Id == _userManager.GetUserId(User)).Single();
                returnAction = "Profile";
            }
               
            else
             patient = _context.Patients.Include(p => p.InsuranceCompany).Include(p => p.User).Single(p => p.Id == model.Id);

            if (ModelState.IsValid)
            {
                try
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
                 
                        InsuranceCompany insurance = _context.InsuranceCompanies.Find(model.SelectedInsuranceCompanyId);

            patient.FirstName = model.FirstName;
            patient.MiddleName = model.MiddleName;
            patient.LastName = model.LastName;
            patient.Gender = model.Gender;
            patient.Image = model.Image;
            patient.Mobile = model.Mobile;
            patient.BloodType = model.BloodType;
            patient.Birthdate = model.Birthdate;
            patient.DisplayName = model.DisplayName;
            patient.Address = model.Address;
             patient.InsuranceCompany = insurance;

                    IdentityUser user = patient.User;
                    var username = await _userManager.GetUserNameAsync(user);
                    if (model.Username != username)
                    {
                        var setUserNameResult = await _userManager.SetUserNameAsync(user, model.Username);

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
                    patient.User = user;


                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
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
            ViewData["Companies"] = getCompanies(patient);
            return View(model);
        }

       

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Assistant,Doctor")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            Patient patient = _context.Patients.Include(p=>p.User).Single(p=>p.Id==id);
            IdentityUser user = patient.User;
            Appointment[] appointments = _context.Appointment.Where(a => a.Patient == patient).ToArray();
           Consultation[] consultations=_context.Consultations.Where(a => a.Patient == patient).ToArray();
            long[] consultationIds = consultations.Select(c => c.Id).ToArray();
            Report[] reports = _context.Report.Where(r => consultationIds.Contains(r.Id)).ToArray();
            Doctor_Patient[] doctor_Patients = _context.Doctor_Patients.Where(d => d.Patient == patient).ToArray();
            if (user != null)
            {
                Reminder[] reminders = _context.Reminders.Where(r => r.User == user).ToArray();
                _context.Reminders.RemoveRange(reminders);
            }
         
            _context.Report.RemoveRange(reports);
            _context.Consultations.RemoveRange(consultations);
            _context.Appointment.RemoveRange(appointments);
            _context.Doctor_Patients.RemoveRange(doctor_Patients);
            _context.Patients.Remove(patient);
         
            await _context.SaveChangesAsync();
            if (user != null)
            {
                await  _userManager.DeleteAsync(user);
            }
               
            return RedirectToAction("Search");
        }

        private bool PatientExists(long id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
      
      
        public List<SelectListItem> getCompanies(Patient patient)
        {
            List<SelectListItem> Companies = new List<SelectListItem>();
            InsuranceCompany[] companies = _context.InsuranceCompanies.ToArray();
         
                foreach (InsuranceCompany c in companies)
                {
          
                    if (patient.InsuranceCompany!=null&& patient.InsuranceCompany.Id == c.Id)
                        Companies.Add(new SelectListItem { Value = "" + c.Id, Text = c.Name, Selected = true });
                    else
                        Companies.Add(new SelectListItem { Value = "" + c.Id, Text = c.Name });
                }
                
          
            return Companies;
        }

      
    }
    }

