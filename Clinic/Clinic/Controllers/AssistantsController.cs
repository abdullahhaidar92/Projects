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
    public class AssistantsController : ClinicController
    {

        public readonly IHostingEnvironment _environment;
        public AssistantsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHostingEnvironment environment)
        : base(context, userManager)
        {
            _environment = environment;
        }
        [Authorize(Roles = "Assistant")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search(SearchAssis search)
        {
            var query = _context.Assistants.Include(d => d.User).Include(a=>a.Doctor).ToArray();
            if (search.SearchId != 0)
            {
                search.Assistants = query.Where(d => d.Id == search.SearchId).ToArray();
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


      [Authorize(Roles="Assistant,Doctor,Admin")]
        public async Task<IActionResult> Profile()
        {
            string id = _userManager.GetUserId(User);
            Assistant assistant;
            if (User.IsInRole("Doctor"))
            {
                assistant = await _context.Assistants.Include(p => p.Doctor).Include(p=>p.User).Where(p=>p.Doctor.User.Id==id)
                                                              .FirstOrDefaultAsync();
                ViewData["Role"] = "Doctor";
            }
            else
            {
                assistant = await _context.Assistants.Include(p => p.Doctor).Include(p => p.User)
                  .FirstOrDefaultAsync(m => m.User.Id == id);
            }
           
            if (assistant == null)
            {
                return NotFound();
            }

            return View(assistant);
        }

        [Authorize(Roles = "Admin,Doctor")]
        public IActionResult Create()
        {
          
            SelectListItem[] Doctors;
            if (User.IsInRole("Doctor"))
            {
               
                try
                {
                    Assistant assis = _context.Assistants.Where(a => a.Doctor.User.Id == _userManager.GetUserId(User)).First();
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
                Doctors[i] = new SelectListItem { Value =""+ IC[i].Id, Text = IC[i].DisplayName };
            }
            ViewData["Doctors"] = Doctors;

            return View();

        }

        // POST: Assistants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> Create(RegisterAssistant registerAssistant, IFormFile file)
        {
            if (_userManager.Users.Any(u => u.UserName == registerAssistant.Username))
            {
                ModelState.AddModelError(String.Empty, "User name already taken");
                ViewData["message"] = "Already Taken";
                ViewData["Doctors"] = GetDoctors(registerAssistant.SelectedDoctorId).ToArray();
                return View(registerAssistant);
            }

            string image = "";
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
               

                var user = new IdentityUser
                { UserName = registerAssistant.Username,
                 Email = registerAssistant.Email,
                 PhoneNumber=registerAssistant.Phone
                };
                var result = await _userManager.CreateAsync(user, registerAssistant.Password);
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Assistant"));
                Doctor I = _context.Doctors.Find(registerAssistant.SelectedDoctorId);
                Assistant p = new Assistant
                {
                    
                    FirstName = registerAssistant.FirstName,
                    MiddleName = registerAssistant.MiddleName,
                    LastName = registerAssistant.LastName,
                    DisplayName = registerAssistant.DisplayName,
                    Address = registerAssistant.Address,
                    Doctor = I,
                    Gender = registerAssistant.Gender,
                    Image = image,
                    User = user
                };

                _context.Add(p);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Search));
            }

            Doctor[] IC = _context.Doctors.ToArray();
            SelectListItem[] Doctors = new SelectListItem[IC.Length];
            for (int i = 0; i < IC.Length; i++)
            {
                Doctors[i] = new SelectListItem { Value = ""+IC[i].Id, Text = IC[i].DisplayName };
            }
            ViewData["Doctors"] = Doctors;
            return View(registerAssistant);
        }

        // GET: Assistants/Edit/5
        [Authorize(Roles = "Admin,Assistant")]
        public IActionResult Edit(long id)
        {
            Assistant assistant = null;
            if (User.IsInRole("Assistant"))
               assistant =_context.Assistants.Where(a=>a.User.Id==_userManager.GetUserId(User)).Include(a => a.Doctor).Include(a => a.User).Single();
            else
                assistant = _context.Assistants.Include(a => a.Doctor).Include(a=>a.User).Single(a => a.Id == id);
            if (assistant == null)
            {
                return NotFound();
            }
            
            ViewData["Doctors"] = GetDoctors(assistant.Doctor.Id);
            EditAssistant model = new EditAssistant
            {
                Id = assistant.Id,
                FirstName = assistant.FirstName,
                MiddleName = assistant.MiddleName,
                LastName = assistant.LastName,
                DisplayName = assistant.DisplayName,
                Address = assistant.Address,
                Gender = assistant.Gender,
                Image = assistant.Image,
                Username = assistant.User.UserName,
                Email=assistant.User.Email,
                Phone=assistant.User.PhoneNumber
  
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Assistant")]
        public async Task<IActionResult> Edit(long id, EditAssistant assistant, IFormFile file)
        {
            string returnAction = "Search";
            Assistant A;
            if (User.IsInRole("Assistant"))
            {
                A = _context.Assistants
                   .Include(a => a.User)
                   .Include(a => a.Doctor)
                   .First(a => a.User.Id == _userManager.GetUserId(User));
                returnAction = "Profile";
            }
            else
            {
             A = _context.Assistants
                    .Include(a => a.User)
                    .Include(a => a.Doctor)
                    .First(a => a.Id == assistant.Id);
            }

            if (id != assistant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  

                    IdentityUser user = A.User;
                    var username = await _userManager.GetUserNameAsync(user);
                    if (assistant.Username != username)
                    {
                        if (_userManager.Users.Any(u => u.UserName == assistant.Username))
                        {
                            ModelState.AddModelError(String.Empty, "User name already taken");
                            ViewData["Doctors"] = GetDoctors(assistant.SelectedDoctorId);
                            return View(assistant);
                        }
                        var setUserNameResult = await _userManager.SetUserNameAsync(user, assistant.Username);

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
                               assistant.Image = file.FileName;
                                file.CopyTo(filestream);
                                filestream.Flush();
                            }
                        }
                  

 
                        A.FirstName = assistant.FirstName;
                        A.MiddleName = assistant.MiddleName;
                        A.LastName = assistant.LastName;
                        A.DisplayName = assistant.DisplayName;
                        A.Address = assistant.Address;
                        A.Gender = assistant.Gender;
                        A.Image = assistant.Image;
                        A.Doctor = _context.Doctors.Find(assistant.SelectedDoctorId);

                    

                    var email = await _userManager.GetEmailAsync(user);
                    if (assistant.Email != email)
                    {
                        var setEmailResult = await _userManager.SetEmailAsync(user, assistant.Email);
                        
                        if (!setEmailResult.Succeeded)
                        {
                            var userId = await _userManager.GetUserIdAsync(user);
                            throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                        }
                    }

                    var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
                    if (assistant.Phone != phoneNumber)
                    {
                        var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, assistant.Phone);
                        if (!setPhoneResult.Succeeded)
                        {
                            var userId = await _userManager.GetUserIdAsync(user);
                            throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                        }
                    }
                    A.User = user;

                        _context.Update(A);
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
                ViewData["Doctors"] = GetDoctors(assistant.SelectedDoctorId);
                return View(assistant);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var assistant = _context.Assistants.Include(a => a.User).Single(a => a.Id == id);
                IdentityUser user = assistant.User;
                _context.Assistants.Remove(assistant);
                await _context.SaveChangesAsync();
                var result =await  _userManager.DeleteAsync(user);
            }
            catch (Exception)
            {
                return RedirectToAction("Search");
            }
            return RedirectToAction("Search");

        }

        private bool AssistantExists(long id)
        {
            return _context.Assistants.Any(e => e.Id == id);
        }

        public List<SelectListItem> GetDoctors(long doctor=0)
        {
            List<SelectListItem> Doctors = new List<SelectListItem>();
          Doctor[] doctors = _context.Doctors.ToArray();

            foreach (Doctor d in doctors)
            {

                if (doctor !=0 && doctor== d.Id)
                    Doctors.Add(new SelectListItem { Value = "" + d.Id, Text = d.DisplayName, Selected = true });
                else
                   Doctors.Add(new SelectListItem { Value = "" + d.Id, Text = d.DisplayName });
            }


            return Doctors;
        }
    }
}
