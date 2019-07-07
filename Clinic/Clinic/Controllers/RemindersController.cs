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
    public class RemindersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public RemindersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

  

   

        [Authorize]
        public IActionResult Create()
        { 
            return View();
        }


        [HttpGet]
        [Authorize]
        public async Task<JsonResult> Create(string Title, DateTime ReminderDate, string Content, int Priority)
        {
            try
            {
                Reminder reminder = new Reminder
                {
                    Title = Title,
                    Date = ReminderDate,
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

        [Authorize]
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
            EditReminder model = new EditReminder
            {
                Id = reminder.Id,
                Title = reminder.Title,
                Date = reminder.Date.ToString("yyyy-MM-dd"),
                Time=reminder.Date.ToString("HH:mm"),
                Priority=reminder.Priority,
                Content=reminder.Content  
            };
            return View(model);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(long id,EditReminder model)
        {
          
            if (id != model.Id)
            {
               
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Reminder reminder = _context.Reminders.Find(id);
                    reminder.Title = model.Title;
                    reminder.Date =DateTime.Parse(model.Date+" "+model.Time);
                    reminder.Content = model.Content;
                    reminder.Priority = model.Priority;
                    _context.Reminders.Update(reminder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReminderExists(model.Id))
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
           
            return View(model);
        }

     

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
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
