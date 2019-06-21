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
        public async Task<IActionResult> Search(SearchPatient search)
        {
            string id = _userManager.GetUserId(User);
            var query = _context.Patients.Include(p => p.InsuranceCompany).ToArray();
            if (User.IsInRole("Doctor"))
            {
                search.EditList = _context.Doctor_Patients.Where(r => r.Doctor.Id == id).Select(r => r.Patient.Id).ToArray();
                if (search.MyPatients)
                    query = query.Where(p => search.EditList.Contains(p.Id)).ToArray();      
            }              
            else 
              if (User.IsInRole("Admin"))
                search.EditList = _context.Patients.Select(p => p.Id).ToArray();

          

         
            if (search.SearchId != 0)
            {
                search.Patients = query.Where(d => d.Patient_Id == search.SearchId).ToArray();
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

        // GET: Patients
        [Authorize(Roles = "Doctor,Admin,Assistant,Insurance")]
        public async Task<IActionResult>All()
        {
            ViewData["Role"] = GetRole();
            if (User.IsInRole("Doctor"))
            {
               
                string[] patientsId =( from r in _context.Doctor_Patients
                                                     where r.Doctor.Id == _userManager.GetUserId(User)
                                                     select r.Patient.Id).ToArray<string>();
                List<Patient> Patients= (from p in _context.Patients
                                                          where patientsId.Contains(p.Id)
                                                           select p).ToList<Patient>();
                return View(Patients);

            }

            if (User.IsInRole("Assistant"))
            {
                Doctor doctor = (_context.Assistants.Where(a => a.Id == _userManager.GetUserId(User)).Include(a => a.Doctor).First()).Doctor;
               
                string[] patientsId =( from r in _context.Doctor_Patients
                                                     where r.Doctor == doctor
                                                     select r.Patient.Id).ToArray<string>();
                List<Patient> Patients= (from p in _context.Patients
                                                          where patientsId.Contains(p.Id)
                                                           select p).ToList<Patient>();
                return View(Patients);

            }

                   if (User.IsInRole("Insurance"))
            {

                List<Patient> Patients = (from p in _context.Patients
                                          where p.InsuranceCompany.Id == _userManager.GetUserId(User)
                                         select p).ToList<Patient>();
               
                return View(Patients);

            }


             
            return View(await _context.Patients.ToListAsync());
        }

        public async Task<IActionResult> Profile()
        {
            string id = _userManager.GetUserId(User);
            var patient = await _context.Patients.Include(p => p.InsuranceCompany)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            long pt_id = 1000;
            try
            {
                pt_id = _context.Patients.Max(d => d.Patient_Id) + 1;
            }
            catch (Exception)
            {
            }
            ViewData["pt_id"] = pt_id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterPatient registerPatient, IFormFile file)
        {
            string image = "avatar.jpg";
            if (_context.Patients.Any(d => d.Patient_Id == registerPatient.Patient_Id))
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
                InsuranceCompanies[0] = new SelectListItem { Value = null, Text = "none" };
                for (int i = 0; i < IC.Length; i++)
                {
                    InsuranceCompanies[i + 1] = new SelectListItem { Value = IC[i].Id, Text = IC[i].Name };
                }
                ViewData["InsuranceCompanies"] = InsuranceCompanies;
               
                return View("~/Views/Patients/FinishCreate.cshtml", registerPatient);
            }

            return View(registerPatient);
        }

        public async Task<IActionResult> FinishCreate(RegisterPatient registerPatient)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registerPatient.Email, Email = registerPatient.Email };
                var result = await _userManager.CreateAsync(user, registerPatient.Password);
                IdentityUser X = await _userManager.FindByEmailAsync(registerPatient.Email);
                await _userManager.AddClaimAsync(X, new Claim(ClaimTypes.Role, "Patient"));
                InsuranceCompany I = _context.InsuranceCompanies.Find(registerPatient.SelectedInsuranceCompanyId);
                Patient p = new Patient
                {
                    Id = user.Id,
                    Email = registerPatient.Email,
                    Patient_Id = registerPatient.Patient_Id,
                    FirstName = registerPatient.FirstName,
                    MiddleName = registerPatient.MiddleName,
                    LastName = registerPatient.LastName,
                    Gender = registerPatient.Gender,
                    Image = registerPatient.Image,
                    Phone = registerPatient.Phone,
                    Mobile = registerPatient.Mobile,
                    BloodType = registerPatient.BloodType,
                    Birthdate = registerPatient.Birthdate,
                    DisplayName = registerPatient.DisplayName,
                    InsuranceCompany = I,
                    Address = registerPatient.Address
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
                InsuranceCompanies[i + 1] = new SelectListItem { Value = IC[i].Id, Text = IC[i].Name };
            }
            ViewData["InsuranceCompanies"] = InsuranceCompanies;
            return View(registerPatient);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (User.IsInRole("Patient"))
            {
                id = _userManager.GetUserId(User);
            }
            if (User.IsInRole("Doctor")&&!_context.Doctor_Patients.Any(r=>r.Doctor.Id== _userManager.GetUserId(User)&& r.Patient.Id==id))
            {
                return View("~/Areas/Identity/Pages/Account/AccessDenied.cshtml");
            }
            if (id == null)
            {
                return NotFound();
            }
            Patient patient;
            try
            {
                patient = _context.Patients.Include(p => p.InsuranceCompany).Single(p => p.Id == id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            ViewData["Companies"] = getCompanies(patient);
            return View(patient);
        }

        // POST: Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Patient patient,IFormFile file,string SelectedInsuranceCompanyId)
        {
            string returnAction = "Search";
            if (User.IsInRole("Patient"))
            {
                id = _userManager.GetUserId(User);
                returnAction = "Profile";
            }
            if (User.IsInRole("Doctor") && !_context.Doctor_Patients.Any(r => r.Doctor.Id == _userManager.GetUserId(User) && r.Patient.Id == id))
            {
                return View("~/Areas/Identity/Pages/Account/AccessDenied.cshtml.cs");
            }

            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        var filePath = Path.GetTempFileName();
                        using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\images\\" + file.FileName))
                        {
                           patient.Image = file.FileName;
                            file.CopyTo(filestream);
                            filestream.Flush();
                        }
                    }
                 
                        patient.InsuranceCompany = _context.InsuranceCompanies.Find(SelectedInsuranceCompanyId);
                  
                    
                        
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
            return View(patient);
        }

       

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Patient patient = await _context.Patients.FindAsync(id);
            IdentityUser user = await _userManager.FindByIdAsync(id);
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

        private bool PatientExists(string id)
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

