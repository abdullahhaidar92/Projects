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
            var admin = await _context.Admins
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
            if (N > 0)
                return View("~/Views/Admins/AdminExsist.cshtml");
            else
            {
                if (ModelState.IsValid)
                {
                    var user = new IdentityUser { UserName = admin.Email, Email = admin.Email };
                    var result = await _userManager.CreateAsync(user, admin.Password);
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin"));
                    Admin A = new Admin
                    {
                        Id = user.Id,
                        FirstName = admin.FirstName,
                        MiddleName = admin.MiddleName,
                        LastName = admin.LastName,
                        Mobile = admin.Mobile,
                        Phone = admin.Phone,
                        Email = admin.Email
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
        public async Task<IActionResult> Edit()
        {
            string id = _userManager.GetUserId(User);
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,MiddleName,LastName,Phone,Mobile")] Admin admin)
        {
            if (id != admin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            return View(admin);
        }

    
        private bool AdminExists(string id)
        {
            return _context.Admins.Any(e => e.Id == id);
        }
    }
}
