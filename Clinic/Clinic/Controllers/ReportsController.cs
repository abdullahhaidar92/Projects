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

namespace Clinic.Controllers
{
    public class ReportsController : ClinicController
    {
        
        public ReportsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        :base(context,userManager)
        {        
        }


        // GET: Reports
        public async Task<IActionResult> Index(SearchReports search)
        {
            string id = _userManager.GetUserId(User);
            var query = _context.Report.Include(d=>d.Doctor)
                                                              .Include(d=>d.Patient)
                                                              .Include(d=>d.Consultation)
                                                              .Where(d=>d.Patient.InsuranceCompany.Id==id)
                                                              .ToArray();
            
            if (search.Patient != null && search.Patient != "all")
                query = query.Where(d => d.Patient.Id == search.Patient).ToArray();
            if (search.Doctor != null && search.Doctor != "all")
                query = query.Where(d => d.Doctor.Id == search.Doctor).ToArray();
            if (search.DateFrom != DateTime.MinValue)
                query = query.Where(d => d.Date >= search.DateFrom).ToArray();
            if (search.DateTo != DateTime.MinValue)
                query = query.Where(d => d.Date <= search.DateTo).ToArray();
            search.Reports= query;
           search.FillPatients(_context.Patients.Where(p=>p.InsuranceCompany.Id==id).ToArray());
            
            search.FillDoctors(_context.Consultations
                                                             .Where(c => c.Patient.InsuranceCompany.Id == id)
                                                             .Select(c => c.Doctor)
                                                             .Distinct()
                                                             .ToArray());
            return View(search);
            
        }

        public async Task<IActionResult> Search(string doctor,string patient,string Order,DateTime reportDate)
        {
            
            string id = _userManager.GetUserId(User);
            if (Order == "asc")
            {
                return View(await _context.Report.Include(r => r.Doctor).Include(r => r.Patient)
                                                                            .Where(r => r.Patient.InsuranceCompany.Id == id)
                                                                            .Where(r => r.Patient.DisplayName == patient
                                                                                           || r.Doctor.DisplayName == doctor
                                                                                           || r.Date == reportDate)
                                                                                           .OrderBy(r => r.Date)
                                                                                           .ToListAsync());

            }
            else
            {
                return View(await _context.Report.Include(r => r.Doctor).Include(r => r.Patient)
                                                                                 .Where(r => r.Patient.InsuranceCompany.Id == id)
                                                                                 .Where(r => r.Patient.DisplayName == patient
                                                                                                || r.Doctor.DisplayName == doctor
                                                                                              || r.Date == reportDate)
                                                                                              .OrderByDescending(r=>r.Date)
                                                                                              .ToListAsync());
            }
           
            
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report.Include(r => r.Doctor).Include(r => r.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Price")] Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }

        
               
        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Date,Price")] Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Id))
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
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var report = await _context.Report.FindAsync(id);
            _context.Report.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(long id)
        {
            return _context.Report.Any(e => e.Id == id);
        }
        private string GetRole()
        {
            if (User.IsInRole("Doctor")) return "Doctor";
            if (User.IsInRole("Admin")) return "Admin";
            if (User.IsInRole("Assistant")) return "Assistant";
            if (User.IsInRole("Patient")) return "Patient";
            if (User.IsInRole("Insurance")) return "Insurance";
            return "";
        }
        public Reminder[] GetReminders()
        {
            return _context.Reminders.Where(r => r.User.Id == _userManager.GetUserId(User))
                                        .ToArray();

        }
    }
}
