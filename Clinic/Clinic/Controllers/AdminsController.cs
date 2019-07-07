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
using System.Security.Claims;

namespace Clinic.Controllers
{
    public class AdminsController : ClinicController
    {
       
        public AdminsController(ApplicationDbContext context, UserManager<IdentityUser> userManager):base(context,userManager)
        {
            
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Authorize(Policy ="Admin")]
        public async Task<IActionResult> Profile()
        {
            string id = _userManager.GetUserId(User);
            var admin = await _context.Admins.Include(a=>a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }
          
            return View(admin);
        }

        public async Task<IActionResult> Create()
        {
            int N = await _context.Admins.CountAsync();
            N = 0;

            if (N > 0)
                return View("~/Views/Admins/AdminExsist.cshtml");
            else
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterAdmin admin)
        {
            int N = await _context.Admins.CountAsync();
            N = 0;

            if (N > 0)
                return View("~/Views/Admins/AdminExsist.cshtml");
            else
            {
                if (ModelState.IsValid)
                {
                    if (_userManager.Users.Any(u => u.UserName == admin.Username))
                    {
                        ModelState.AddModelError(String.Empty, "User name already taken");
                        ViewData["message"] = "Already Taken";
                        return View(admin);
                    }


                    var user = new IdentityUser { UserName = admin.Username, Email = admin.Email,PhoneNumber=admin.Phone };
                    var result = await _userManager.CreateAsync(user, admin.Password);
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin"));
                    Admin A = new Admin
                    {
                        Id = user.Id,
                        FirstName = admin.FirstName,
                        MiddleName = admin.MiddleName,
                        LastName = admin.LastName,
                        Mobile = admin.Mobile,
                        User=user
                    };
                    _context.Admins.Add(A);
                    _context.SaveChanges();
                    if (result.Succeeded)
                    {
                        return RedirectToRoute("Admin");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                // If we got this far, something failed, redisplay form
                return View(admin);
            }
              

      

        } 

        [Authorize(Policy = "Admin")]
        public IActionResult Edit()
        {
            string id = _userManager.GetUserId(User);
            if (id == null)
            {
                return NotFound();
            }

            var admin =  _context.Admins.Include(a=>a.User).Single(a=>a.Id==id);
            if (admin == null)
            {
                return NotFound();
            }
            EditAdmin model = new EditAdmin
            {
                Id =admin.Id,
                FirstName = admin.FirstName,
                MiddleName = admin.MiddleName,
                LastName = admin.LastName,
                Mobile = admin.Mobile,
                Username=admin.User.UserName,
                Email=admin.User.Email,
                Phone=admin.User.PhoneNumber
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(string id,EditAdmin model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
         

            Admin admin = _context.Admins.Include(a=>a.User).Single(a=>a.Id==id);
            if (admin == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                try
                {
                    IdentityUser user = admin.User;
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

                    admin.FirstName = model.FirstName;
                    admin.MiddleName = model.MiddleName;
                    admin.LastName = model.LastName;
                        admin.Mobile = model.Mobile;
                   

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
                 
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Id))
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
            return View(model);
        }

    
        private bool AdminExists(string id)
        {
            return _context.Admins.Any(e => e.Id == id);
        }
    }
}
