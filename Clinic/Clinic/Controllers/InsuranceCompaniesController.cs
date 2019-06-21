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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Clinic.Controllers
{
    public class InsuranceCompaniesController : ClinicController
    {

        public readonly IHostingEnvironment _environment;
        public InsuranceCompaniesController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHostingEnvironment environment)
        : base(context, userManager)
        {
            _environment = environment;
        }
        [Authorize(Policy ="Insurance")]
        public async Task<IActionResult> Index()
        {
            return View();
        }


            public async Task<IActionResult> Search(SearchInsurance search)
        {

            var query = _context.InsuranceCompanies.Where(i=>i.Insurance_Id>0).ToArray();
            if (search.SearchId != 0)
            {
                search.InsuranceCompanies = query.Where(d => d.Insurance_Id == search.SearchId).ToArray();
                return View(search);
            }
            if (search.Name != null)
                query = query.Where(d => d.Name == search.Name).ToArray();
            if (search.Email != null)
                query = query.Where(d => d.Email == search.Email).ToArray();
            if (search.Address != null)
                query = query.Where(d => d.Address.Contains(search.Address)).ToArray();
            if (search.Order == "desc")
                query = query.OrderByDescending(d => d.Name).ToArray();
            else
                query = query.OrderBy(d => d.Name).ToArray();
            search.InsuranceCompanies = query;
            return View(search);
        }
        // GET: InsuranceCompanies
        

        // GET: InsuranceCompanies/Details/5
        public async Task<IActionResult> Profile()
        {
            string id = _userManager.GetUserId(User);
            var insurance = await _context.InsuranceCompanies.FirstOrDefaultAsync(m => m.Id == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // GET: InsuranceCompanies/Create
        public IActionResult Create()
        {
            long in_id = 1000;
            try
            {
                in_id = _context.InsuranceCompanies.Max(d => d.Insurance_Id) + 1;
            }
            catch (Exception)
            {
            }
            ViewData["in_id"] = in_id;
            return View();
        }

        // POST: InsuranceCompanies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterInsuranceCompany RegisterInsuranceCompany, IFormFile file)
        {
            if (ModelState.IsValid)
            {

                var filePath = Path.GetTempFileName();

                string image = "";

                using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\images\\" + file.FileName))
                {
                    image = file.FileName;
                    file.CopyTo(filestream);
                    filestream.Flush();
                }
                var user = new IdentityUser { UserName = RegisterInsuranceCompany.Email, Email = RegisterInsuranceCompany.Email };
                var result = await _userManager.CreateAsync(user, RegisterInsuranceCompany.Password);
                IdentityUser X = await _userManager.FindByEmailAsync(RegisterInsuranceCompany.Email);
                await _userManager.AddClaimAsync(X, new Claim(ClaimTypes.Role, "Insurance"));
                RegisterInsuranceCompany.InsuranceCompany.Id = user.Id;
                RegisterInsuranceCompany.InsuranceCompany.Image = image;
                RegisterInsuranceCompany.InsuranceCompany.Email = RegisterInsuranceCompany.Email;
                _context.Add(RegisterInsuranceCompany.InsuranceCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Search));
            }
            ViewData["error"] = "error";
            return View(RegisterInsuranceCompany);
        }

        // GET: InsuranceCompanies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
        
            if (User.IsInRole("Insurance"))
            {
                id = _userManager.GetUserId(User);
              
            }

            if (id == null)
            {
                return NotFound();
            }

            var insuranceCompany = await _context.InsuranceCompanies.FindAsync(id);
            if (insuranceCompany == null)
            {
                return NotFound();
            }
            return View(insuranceCompany);
        }

        // POST: InsuranceCompanies/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Phone,Address,Fax")] InsuranceCompany insuranceCompany,IFormFile file)
        {
            string returnAction = "Search";
         
            if (User.IsInRole("Insurance"))
            {
                id = _userManager.GetUserId(User);
                returnAction = "Profile";
            }

            if (id != insuranceCompany.Id)
            {
                return NotFound();
            }
            insuranceCompany.Image = _context.InsuranceCompanies.Where(i=>i.Id==id).Select(r=>r.Image).Single();
            if (ModelState.IsValid)
            {
                try
                {
                  
                    if (file != null)
                    {
                        var filePath = Path.GetTempFileName();
                        using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\images\\" + file.FileName))
                        {
                            insuranceCompany.Image = file.FileName;
                            file.CopyTo(filestream);
                            filestream.Flush();
                        }
                    }
                    _context.InsuranceCompanies.Update(insuranceCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceCompanyExists(insuranceCompany.Id))
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
            return View(insuranceCompany);
        }


        // POST: InsuranceCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
           InsuranceCompany company = await _context.InsuranceCompanies.FindAsync(id);
            IdentityUser user = await _userManager.FindByIdAsync(id);
            Patient[] patients = _context.Patients.Where(a => a.InsuranceCompany== company).ToArray();
            if (user != null)
            {
                Reminder[] reminders = _context.Reminders.Where(r => r.User == user).ToArray();
                _context.Reminders.RemoveRange(reminders);
            }
            InsuranceCompany None = _context.InsuranceCompanies.Find("none");
            foreach (Patient p in patients)
                p.InsuranceCompany = None;
            _context.Patients.UpdateRange(patients);
            _context.InsuranceCompanies.Remove(company);

            await _context.SaveChangesAsync();
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Search");
        }

        private bool InsuranceCompanyExists(string id)
        {
            return _context.InsuranceCompanies.Any(e => e.Id == id);
        }
    }
}
