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
using Microsoft.AspNetCore.Authorization;

namespace Clinic.Controllers
{
    public class ReportsController : ClinicController
    {
        
        public ReportsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        :base(context,userManager)
        {        
        }


        [Authorize(Roles="Insurance")]
        public async Task<IActionResult> Index(SearchReports search)
        {
            string id = _userManager.GetUserId(User);
            var query = _context.Report.Include(d=>d.Doctor)
                                                              .Include(d=>d.Patient)
                                                              .Include(d=>d.Consultation)
                                                              .Where(d=>d.Patient.InsuranceCompany.User.Id==id)
                                                              .ToArray();
            
            if (search.Patient != 0)
                query = query.Where(d => d.Patient.Id == search.Patient).ToArray();
            if (search.Doctor != 0)
                query = query.Where(d => d.Doctor.Id == search.Doctor).ToArray();
            if (search.DateFrom != DateTime.MinValue)
                query = query.Where(d => d.Date >= search.DateFrom).ToArray();
            if (search.DateTo != DateTime.MinValue)
                query = query.Where(d => d.Date <= search.DateTo).ToArray();
            else
                search.DateTo = DateTime.Now;
            search.Reports= query;
           search.FillPatients(_context.Patients.Where(p=>p.InsuranceCompany.User.Id==id).ToArray());
            
            search.FillDoctors(_context.Consultations
                                                             .Where(c => c.Patient.InsuranceCompany.User.Id == id)
                                                             .Select(c => c.Doctor)
                                                             .Distinct()
                                                             .ToArray());
            return View(search);
            
        }

    
        [Authorize(Roles ="Insurance")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var report = await _context.Report.FindAsync(id);
            _context.Report.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

  
       
    }
}
