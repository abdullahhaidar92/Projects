using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic.Data;
using Clinic.Models;
using Microsoft.AspNetCore.Authorization;

namespace Clinic.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(SearchMessages search)
        {
            var query = _context.Messages.ToArray();

            if (search.DateFrom != DateTime.MinValue)
                query = query.Where(d => d.DateTime >= search.DateFrom).ToArray();
            if (search.DateTo != DateTime.MinValue)
                query = query.Where(d => d.DateTime <= search.DateTo).ToArray();
            if (search.Subject != null)
                query = query.Where(d => d.Subject == search.Subject).ToArray();
            if (search.Email != null)
                query = query.Where(d => d.Email == search.Email).ToArray();
            search.Messages = query;

            if (search.DateTo == DateTime.MinValue)
                search.DateTo = DateTime.Now.Date;
                return View(search);
        }




        public IActionResult Create()
        {
            return View();
        }


      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Subject,Content,DateTime")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.DateTime = DateTime.Now;
                _context.Add(message);
                await _context.SaveChangesAsync();
                return View("/Views/Messages/Done.cshtml");
            }
            return View(message);
        }



        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var message = await _context.Messages.FindAsync(id);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(long id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}
