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
    public class RemindersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public RemindersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Reminders
        public async Task<IActionResult> Index()
        {


            return View(await _context.Reminders.
                                  Where(r => r.User.Id == _userManager.GetUserId(User)).ToListAsync());

        }

        // GET: Reminders/Details/5
        public async Task<IActionResult> Details(long? id)
        {



            if (id == null)
            {
                return NotFound();
            }

            var reminder = await _context.Reminders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reminder == null)
            {
                return NotFound();
            }

            return View(reminder);
        }

        // GET: Reminders/Create
        public IActionResult Create()
        {



            return View();
        }


        [HttpGet]

        public async Task<JsonResult> Create(string Title, DateTime Time, string Content, int Priority)
        {
            try
            {
                Reminder reminder = new Reminder
                {
                    Title = Title,
                    Date = Time,
                    Content = Content,
                    Priority = Priority
                };

                reminder.User =await  _userManager.GetUserAsync(User);
                _context.Add(reminder);
                 await  _context.SaveChangesAsync();
                return Json(new { Result = "Reminder created successfully " });

            }
            catch(Exception e)

            {
                return Json(new { Result = e.ToString() });
            }
        
            
        }

        // GET: Reminders/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
           
           

            if (id == null)
            {
                return NotFound();
            }

            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }
      
            return View(reminder);
        }

        // POST: Reminders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,Date,Content,Priority")] Reminder reminder)
        {
          
            if (id != reminder.Id)
            {
               
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Reminders.Update(reminder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReminderExists(reminder.Id))
                    {
                        
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToRoute("Home");
            }
           
            return View(reminder);
        }

     

        // POST: Reminders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
           
            var reminder = await _context.Reminders.FindAsync(id);
            _context.Reminders.Remove(reminder);
            await _context.SaveChangesAsync();
            return RedirectToRoute("Home");
        }

        private bool ReminderExists(long id)
        {
            return _context.Reminders.Any(e => e.Id == id);
        }

        private string GetRole()
        {
            if (User.IsInRole("Doctor"))  return  "Doctor";
            if (User.IsInRole("Admin")) return "Admin";
            if (User.IsInRole("Assistant")) return "Assistant";
            if (User.IsInRole("Patient"))  return"Doctor";
            return "";

        }
        public Reminder[] GetReminders()
        {
            return _context.Reminders.Where(r => r.User.Id == _userManager.GetUserId(User))
                                        .OrderBy(r=>r.Priority).ToArray();

        }
    }
}
