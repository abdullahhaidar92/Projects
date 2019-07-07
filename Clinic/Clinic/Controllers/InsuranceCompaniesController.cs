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

        [Authorize(Roles="Admin")]
            public async Task<IActionResult> Search(SearchInsurance search)
        {

            var query = _context.InsuranceCompanies.Include(d => d.User).Where(i=>i.Id>0).ToArray();
            if (search.SearchId != 0)
            {
                search.InsuranceCompanies = query.Where(d => d.Id == search.SearchId).ToArray();
                return View(search);
            }
            if (search.Name != null)
                query = query.Where(d => d.Name == search.Name).ToArray();
            if (search.Email != null)
                query = query.Where(d => d.User.Email == search.Email).ToArray();
            if (search.Address != null)
                query = query.Where(d => d.Address.Contains(search.Address)).ToArray();
            if (search.Order == "desc")
                query = query.OrderByDescending(d => d.Name).ToArray();
            else
                query = query.OrderBy(d => d.Name).ToArray();
            search.InsuranceCompanies = query;
            return View(search);
        }

        [Authorize(Roles = "Insurance")]
        public async Task<IActionResult> Profile()
        {
            string id = _userManager.GetUserId(User);
            var insurance = await _context.InsuranceCompanies.Include(i=>i.User).FirstOrDefaultAsync(i=> i.User.Id == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
                   return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(RegisterInsuranceCompany registerInsuranceCompany, IFormFile file)
        {
            if (_userManager.Users.Any(u => u.UserName == registerInsuranceCompany.Username))
            {
                ModelState.AddModelError(String.Empty, "User name already taken");
                ViewData["message"] = "Already Taken";
                return View(registerInsuranceCompany);
            }

            if (ModelState.IsValid)
            {
                if(_context.InsuranceCompanies.Any(i=>i.User.UserName==registerInsuranceCompany.Username))
                {
                    ViewData["message"] = "Already Taken";
                    return View(registerInsuranceCompany);
                }

               

                string image = "";
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
             
                var user = new IdentityUser
                {
                    UserName = registerInsuranceCompany.Username,
                    Email = registerInsuranceCompany.Email,
                    PhoneNumber=registerInsuranceCompany.Phone
                };
                var result = await _userManager.CreateAsync(user, registerInsuranceCompany.Password);
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Insurance"));

                InsuranceCompany company = new InsuranceCompany
                {
                    Name = registerInsuranceCompany.Name,
                    Address = registerInsuranceCompany.Address,
                    Fax = registerInsuranceCompany.Fax,
                    Image = image,
                    User = user
                };
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Search));
            }
            ViewData["error"] = "error";
            return View(registerInsuranceCompany);
        }

        [Authorize(Roles = "Admin,Insurance")]
        public async Task<IActionResult> Edit(long id)
        {
            InsuranceCompany insuranceCompany = null;

            if (User.IsInRole("Insurance"))
               insuranceCompany=_context.InsuranceCompanies.Include(i=>i.User).Where(i=>i.User.Id==_userManager.GetUserId(User)).Single();
              else
             insuranceCompany = _context.InsuranceCompanies.Include(i => i.User).Single(i=>i.Id==id);

            if (insuranceCompany == null)
            {
                return NotFound();
            }
            EditInsuranceCompany model = new EditInsuranceCompany
            {
                Id = insuranceCompany.Id,
                Name =insuranceCompany.Name,
                Address = insuranceCompany.Address,
                Fax = insuranceCompany.Fax,
                Image =insuranceCompany.Image,
                Username=insuranceCompany.User.UserName,
                Email=insuranceCompany.User.Email,
                Phone=insuranceCompany.User.PhoneNumber
            };
            return View(model);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Insurance")]
        public async Task<IActionResult> Edit(long id, EditInsuranceCompany model,IFormFile file)
        {
            string returnAction = "Search";
            if (_userManager.Users.Any(u => u.UserName == model.Username))
            {
                ModelState.AddModelError(String.Empty, "User name already taken");
                return View(model);
            }
            if (id != model.Id)
            {
                return NotFound();
            }

            InsuranceCompany company = _context.InsuranceCompanies.Include(i => i.User).Single(i => i.Id == id);
          
            if (ModelState.IsValid)
            {
                try
                {
                    IdentityUser user = company.User;
                    var username = await _userManager.GetUserNameAsync(user);
                    if (model.Username != username)
                    {
                        if (_userManager.Users.Any(u => u.UserName == model.Username))
                        {
                            ModelState.AddModelError(String.Empty, "User name already taken");
                            return View(model);
                        }
                        var setUserNameResult = await _userManager.SetUserNameAsync(user, model.Username);

                        if (!setUserNameResult.Succeeded)
                        {
                            var userId = await _userManager.GetUserIdAsync(user);
                            throw new InvalidOperationException($"Unexpected error occurred setting username for user with ID '{userId}'.");
                        }
                    }

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

                    company.Name = model.Name;
                company.Address = model.Address;
               company.Fax = model.Fax;
                company.Image = model.Image;

                   

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
                    company.User = user;

                    _context.InsuranceCompanies.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceCompanyExists(company.Id))
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


        // POST: InsuranceCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            InsuranceCompany company = _context.InsuranceCompanies.Include(i => i.User).Single(i => i.Id == id);
            IdentityUser user = company.User;
            Patient[] patients = _context.Patients.Include(p=>p.InsuranceCompany).Where(a => a.InsuranceCompany== company).ToArray();
            if (user != null)
            {
                Reminder[] reminders = _context.Reminders.Where(r => r.User == user).ToArray();
                _context.Reminders.RemoveRange(reminders);
            }
          
            foreach (Patient p in patients)
                p.InsuranceCompany = null;
            _context.Patients.UpdateRange(patients);
            _context.InsuranceCompanies.Remove(company);

            await _context.SaveChangesAsync();
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Search");
        }

        private bool InsuranceCompanyExists(long id)
        {
            return _context.InsuranceCompanies.Any(e => e.Id == id);
        }
    }
}
