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

namespace Clinic.Controllers
{
    public class AssistantsController : ClinicController
    {

        public readonly IHostingEnvironment _environment;
        public AssistantsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHostingEnvironment environment)
        : base(context, userManager)
        {
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(SearchAssis search)
        {
            var query = _context.Assistants.Include(a=>a.Doctor).ToArray();
            if (search.SearchId != 0)
            {
                search.Assistants = query.Where(d => d.Assistant_Id == search.SearchId).ToArray();
                return View(search);
            }
            if (search.FirstName != null)
                query = query.Where(d => d.FirstName == search.FirstName).ToArray();
            if (search.MiddleName != null)
                query = query.Where(d => d.MiddleName == search.MiddleName).ToArray();
            if (search.LastName != null)
                query = query.Where(d => d.LastName == search.LastName).ToArray();
            if (search.Doctor != null)
                query = query.Where(d => d.Doctor.DisplayName == search.Doctor).ToArray();
            if (search.Order == "desc")
                query = query.OrderByDescending(d => d.FirstName).ToArray();
            else
                query = query.OrderBy(d => d.FirstName).ToArray();
            search.Assistants = query;
            return View(search);
        }

     

        // GET: Assistants/Details/5
        public async Task<IActionResult> Profile()
        {
            string id = _userManager.GetUserId(User);
            Assistant assistant;
            if (User.IsInRole("Doctor"))
            {
                assistant = await _context.Assistants.Include(p => p.Doctor).Where(p=>p.Doctor.Id==id)
                                                              .FirstOrDefaultAsync();
                ViewData["Role"] = "Doctor";
            }
            else
            {
                assistant = await _context.Assistants.Include(p => p.Doctor)
                  .FirstOrDefaultAsync(m => m.Id == id);
            }
           
            if (assistant == null)
            {
                return NotFound();
            }

            return View(assistant);
        }

        // GET: Assistants/Create
        public IActionResult Create()
        {
            long as_id = 1000;
            try
            {
                as_id = _context.Assistants.Max(d => d.Assistant_Id) + 1;
            }
            catch (Exception)
            {
            }
            ViewData["as_id"] = as_id;


            SelectListItem[] Doctors;
            if (User.IsInRole("Doctor"))
            {
               
                try
                {
                    Assistant assis = _context.Assistants.Where(a => a.Doctor.Id == _userManager.GetUserId(User)).First();
                    return View("/Views/Assistants/Details.cshtml", assis);
                }
                catch (Exception)
                {
                    Doctors = new SelectListItem[] { new SelectListItem { Value = _userManager.GetUserId(User), Text = "Me" } };
                    ViewData["Doctors"] = Doctors;
                    return View();
                }

            }

            Doctor[] IC = _context.Doctors.ToArray();
            Doctors = new SelectListItem[IC.Length];
            for (int i = 0; i < IC.Length; i++)
            {
                Doctors[i] = new SelectListItem { Value = IC[i].Id, Text = IC[i].DisplayName };
            }
            ViewData["Doctors"] = Doctors;

            return View();

        }

        // POST: Assistants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterAssistant registerAssistant, IFormFile file)
        {
            if (_context.Assistants.Any(d => d.Assistant_Id== registerAssistant.Assistant_Id))
            {
                ViewData["message"] = "Already Taken";
                return View(registerAssistant);
            }
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

                var user = new IdentityUser { UserName = registerAssistant.Email, Email = registerAssistant.Email };
                var result = await _userManager.CreateAsync(user, registerAssistant.Password);
                IdentityUser X = await _userManager.FindByEmailAsync(registerAssistant.Email);
                await _userManager.AddClaimAsync(X, new Claim(ClaimTypes.Role, "Assistant"));
                Doctor I = _context.Doctors.Find(registerAssistant.SelectedDoctorId);
                Assistant p = new Assistant
                {
                    Id = user.Id,
                    Assistant_Id=registerAssistant.Assistant_Id,
                    FirstName=registerAssistant.FirstName,
                    MiddleName=registerAssistant.MiddleName,
                    LastName=registerAssistant.LastName,
                    DisplayName = registerAssistant.DisplayName,
                    Address = registerAssistant.Address,
                    Phone = registerAssistant.Phone,
                    Email=registerAssistant.Email,
                    Doctor = I,
                    Gender = registerAssistant.Gender,
                    Image=image
                };

                _context.Add(p);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Search));
            }
            Doctor[] IC = _context.Doctors.ToArray();
            SelectListItem[] Doctors = new SelectListItem[IC.Length];
            for (int i = 0; i < IC.Length; i++)
            {
                Doctors[i] = new SelectListItem { Value = IC[i].Id, Text = IC[i].DisplayName };
            }
            ViewData["Doctors"] = Doctors;
            return View(registerAssistant);
        }

        // GET: Assistants/Edit/5
        public IActionResult Edit(string id)
        {
            if (User.IsInRole("Assistant"))
            {
                id = _userManager.GetUserId(User);
            }

            if (id == null)
            {
                return NotFound();
            }

            var assistant = _context.Assistants.Include(a => a.Doctor).Single(a => a.Id == id);
            ViewData["Doctors"] = GetDoctors(assistant);
            if (assistant == null)
            {
                return NotFound();
            }
            return View(assistant);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Assistant assistant, IFormFile file, string SelectedDoctorId)
        {
            string returnAction = "Search";
            if (User.IsInRole("Assistant"))
            {
                id = _userManager.GetUserId(User);
                returnAction = "Profile";
            }

            if (id != assistant.Id)
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
                               assistant.Image = file.FileName;
                                file.CopyTo(filestream);
                                filestream.Flush();
                            }
                        }
                    assistant.Doctor = _context.Doctors.Find(SelectedDoctorId);
                        _context.Update(assistant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssistantExists(assistant.Id))
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
                ViewData["Doctors"] = GetDoctors(assistant);
                return View(assistant);
        }

        // GET: Assistants/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assistant = await _context.Assistants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assistant == null)
            {
                return NotFound();
            }

            return View(assistant);
        }

        // POST: Assistants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var assistant = await _context.Assistants.FindAsync(id);
            _context.Assistants.Remove(assistant);
            await _context.SaveChangesAsync();
            IdentityUser user= await _userManager.FindByIdAsync(id);
               var result= _userManager.DeleteAsync(user);
          
            return RedirectToAction(nameof(Search));
        }

        private bool AssistantExists(string id)
        {
            return _context.Assistants.Any(e => e.Id == id);
        }

        public List<SelectListItem> GetDoctors(Assistant assistant)
        {
            List<SelectListItem> Doctors = new List<SelectListItem>();
          Doctor[] doctors = _context.Doctors.ToArray();

            foreach (Doctor d in doctors)
            {

                if (assistant.Doctor != null && assistant.Doctor.Id== d.Id)
                    Doctors.Add(new SelectListItem { Value = "" + d.Id, Text = d.DisplayName, Selected = true });
                else
                   Doctors.Add(new SelectListItem { Value = "" + d.Id, Text = d.DisplayName });
            }


            return Doctors;
        }
    }
}
