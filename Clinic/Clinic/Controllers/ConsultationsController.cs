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
    public class ConsultationsController : ClinicController
    {
     
 public ConsultationsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
            :base( context, userManager)
        {
           
        }
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> Index()
        {
             string Id = _userManager.GetUserId(User);

            if (User.IsInRole("Doctor"))
            {
               
                return View(await _context.Consultations.Where(c=>c.Doctor.Id==Id)
                                                            .Include(c => c.Patient).ToListAsync());
            }
          
            return View(await _context.Consultations.Include(c => c.Patient).Include(c=>c.Doctor).ToListAsync());
        }

        [Authorize(Policy = "Patient")]
        public async Task<IActionResult> SearchPatientsConsultations(SearchConsultaion search)
        {
            var query = _context.Consultations.Where(c=>c.Patient.Id == _userManager.GetUserId(User))
                                                                           .Include(a => a.Doctor).ToArray();

            if (search.Doctor != null && search.Doctor != "all")
                query = query.Where(d => d.Doctor.Id == search.Doctor).ToArray();
            if (search.Title != null)
                query = query.Where(d => d.Title == search.Title).ToArray();
            if (search.Type != null)
                query = query.Where(d => d.Type == search.Type).ToArray();
            if (search.Order == "desc")
                query = query.OrderByDescending(d => d.Date).ToArray();
            else
                query = query.OrderBy(d => d.Date).ToArray();
            search.Consultations = query;

            search.FillDoctors(_context.Doctor_Patients.Where(r => r.Patient.Id == _userManager.GetUserId(User))
                                                                                            .Select(r => r.Doctor).ToArray());
            return View(search);
        }

        [Authorize(Policy = "Doctor")]
        public async Task<IActionResult> SearchDoctorsConsultations(SearchConsultaion search)
        {
            var query = _context.Consultations.Where(c => c.Doctor.Id == _userManager.GetUserId(User))
                                                                            .Include(a => a.Patient).ToArray();

            if (search.Patient != "all")
                query = query.Where(d => d.Patient.Id == search.Patient).ToArray();
            if (search.Title != null)
                query = query.Where(d => d.Title == search.Title).ToArray();
            if (search.Type != null)
                query = query.Where(d => d.Type == search.Type).ToArray();
            if (search.Order == "desc")
                query = query.OrderByDescending(d => d.Date).ToArray();
            else
                query = query.OrderBy(d => d.Date).ToArray();
            search.Consultations = query;

            search.FillPatients(_context.Doctor_Patients.Where(r=>r.Doctor.Id== _userManager.GetUserId(User))
                                                                                            .Select(r=>r.Patient).ToArray());
            return View(search);
        }

        [Authorize(Roles = "Patient,Doctor")]
        public async Task<IActionResult> Details(long? id)
        {
           

            if (id == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

       
        [Authorize(Policy="Doctor")]
        public IActionResult Create()
        {
            AddConsultation model = new AddConsultation();
            Patient[] Ps = _context.Patients.ToArray();
            model.Patients = new SelectListItem[Ps.Length];
            for (int i = 0; i <model.Patients.Length; i++)
            {
               model.Patients[i] = new SelectListItem { Value = Ps[i].Id, Text = Ps[i].DisplayName };
            }
           
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Doctor")]
        public async Task<IActionResult> Create(AddConsultation addConsultation)
        {
           

            if (ModelState.IsValid)
            {
                Consultation consultation = new Consultation()
                {
                   Patient = _context.Patients.Where(p => p.Id == addConsultation.SelectedPatientId)
                                                                    .Include(p=>p.InsuranceCompany).Single<Patient>(),
                    Doctor = _context.Doctors.Find(_userManager.GetUserId(User)),
                    Title = addConsultation.Title,
                    Type=addConsultation.Type,
                    Date = DateTime.Now,
                    Temperature = addConsultation.Temperature,
                    BloodPressure=addConsultation.BloodPressure,
                    Symptoms =addConsultation.Symptoms,
                    Diagnosis = addConsultation.Diagnosis,
                    Treatment= addConsultation.Treatment,
                    Cost = addConsultation.Cost,
                    InsuranceConfirmation = addConsultation.InsuranceConfirmation
                 };
                _context.Add(consultation);
             
                Doctor_Patient Relation = new Doctor_Patient()
                {
                    Doctor = consultation.Doctor,
                    Patient = consultation.Patient
                };
                _context.Add(Relation);

                Report report = new Report()
                {
                    Date = consultation.Date,
                    Price = consultation.Cost,
                    Doctor = consultation.Doctor,
                    Patient = consultation.Patient,
                    Consultation = consultation,
                    Insurance = consultation.Patient.InsuranceCompany

                };
                _context.Add(report);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            Patient[] Ps = _context.Patients.ToArray();
            SelectListItem[] Patients = new SelectListItem[Ps.Length + 1];
            Patients[0] = new SelectListItem { Value = "", Text = "Select the patient" };
            for (int i = 0; i < Patients.Length - 1; i++)
            {
                Patients[i + 1] = new SelectListItem { Value = Ps[i].Id, Text = Ps[i].DisplayName };
            }
            ViewData["Patients"] = Patients;
            return View(addConsultation);
        }

        [Authorize(Policy = "Doctor")]
        public async Task<IActionResult> Edit(long? id)
        {
           

            if (id == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultations.Where(c=>c.Id==id).FirstAsync();
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Doctor")]
        public async Task<IActionResult> Edit(long id, Consultation consultation)
        {
           

            if (id != consultation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(consultation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultationExists(consultation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(consultation);
        }

        [Authorize(Policy = "Doctor")]
        public async Task<IActionResult> Delete(long? id)
        {
           

            if (id == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Doctor")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
           

            var consultation = await _context.Consultations.FindAsync(id);
            _context.Consultations.Remove(consultation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultationExists(long id)
        {
            return _context.Consultations.Any(e => e.Id == id);
        }
       
    }
}
