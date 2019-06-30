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
    public class AppointmentsController : ClinicController
    {
         
        public AppointmentsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        :base(context,userManager)
        {                 
        }

        [Authorize(Policy ="Assistant")]
        public async Task<IActionResult> Index(SearchAppointments search)
        {
            var query = _context.Appointment.Include(a => a.Patient).ToArray();
            if (search.Patient !=0)
                query = query.Where(a => a.Patient.Id == search.Patient).ToArray();
            if (search.DateTime!= DateTime.MinValue)
                query = query.Where(a => a.DateTime.Date == search.DateTime.Date).ToArray();
            search.FillPatients(_context.Patients.ToArray());
            search.Appointments = query.OrderBy(a=>a.DateTime).ToArray();
            return View(search);
        }

        [Authorize(Policy = "Patient")]
        public async Task<IActionResult> PatientsAppointments(SearchAppointments search)
        {
            string id = _userManager.GetUserId(User);
            var query = _context.Appointment.Include(a => a.Doctor).Where(a=>a.Patient.User.Id==id).ToArray();
            if (search.Doctor !=0)
                query = query.Where(a => a.Doctor.Id == search.Doctor).ToArray();
            if (search.DateTime != DateTime.MinValue)
                query = query.Where(a => a.DateTime.Date == search.DateTime.Date).ToArray();
            search.FillDoctors(_context.Doctors.ToArray());
            search.Appointments = query;
            return View(search);
        }

        [Authorize(Policy = "Doctor")]
        public async Task<IActionResult> DoctorsAppointments(SearchAppointments search)
        {
            string id = _userManager.GetUserId(User);
            var query = _context.Appointment.Include(a => a.Patient).Where(a => a.Doctor.User.Id == id).ToArray();
            if (search.Patient != 0)
                query = query.Where(a => a.Patient.Id == search.Patient).ToArray();
            if (search.DateTime != DateTime.MinValue)
                query = query.Where(a => a.DateTime.Date == search.DateTime.Date).ToArray();
            search.FillPatients(_context.Patients.ToArray());
            search.Appointments = query;
            return View(search);
        }


        [Authorize(Policy ="Assistant")]
        public IActionResult Create()
        {
            return View(new AddAppointment(_context));
        }

        
        [HttpGet]
        public  JsonResult Create(long patient, DateTime appdate)
        {
            Doctor doctor;
            try
            {
                 doctor = _context.Assistants.Include(a=>a.Doctor).First(a=>a.User.Id==_userManager.GetUserId(User)).Doctor;
            }
            catch (Exception)
            {
                return Json(new { Result = "No Doctor " });
            }
            if (doctor == null)
            {
                return Json(new { Result = "No Doctor " });

            }
            if (ModelState.IsValid)
                {
                    if (_context.Appointment.Any(a => a.DateTime == appdate && a.Doctor == doctor))
                    {
                        return Json(new { Result = "This period is not available " });
                    }

                    Appointment appointment = new Appointment()
                    {
                        DateTime = appdate,
                        Doctor = doctor,
                        Patient = _context.Patients.Find(patient)

                    };
                    _context.Appointment.Add(appointment);
                    _context.SaveChanges();

                    return Json(new { Result = "Appointment successfully added " });
                }
                return Json(new { Result = "Something wrong happened " });
        
          
          
        }

        [Authorize(Policy = "Assistant")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Appointment appointment = _context.Appointment.Include(a=>a.Patient).Where(a=>a.Id==id).Single<Appointment>();
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["id"] = id;
            AddAppointment A = new AddAppointment(appointment.Patient.Id, _context);
            A.DateTime = appointment.DateTime;
            return View(A);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Assistant")]
        public async Task<IActionResult> Edit(long id,AddAppointment addAppointment)
        {
            Appointment appointment = new Appointment();
            Doctor d;
            try
            {
                d = _context.Assistants.Include(a => a.Doctor).First(a => a.User.Id == _userManager.GetUserId(User)).Doctor;
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
           
            _context.Appointment.Remove(_context.Appointment.Find(id));
            _context.SaveChanges();
            if (ModelState.IsValid)
            {
                try
                {

                    if (_context.Appointment.Any(a => a.DateTime == addAppointment.DateTime )) 
                    {
                        ViewData["Message"] = "This period is reserved for another doctor";
                        return View(new AddAppointment(addAppointment.SelectPatientId, _context));
                    }
                    appointment.Doctor = d;
                    appointment.DateTime = addAppointment.DateTime;
                    appointment.Patient = _context.Patients.Find(addAppointment.SelectPatientId); 
                    _context.Appointment.Add(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(id))
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
           

            AddAppointment A = new AddAppointment(addAppointment.SelectPatientId, _context);
            A.DateTime = DateTime.Now;
            ViewData["id"] = id;
            return View(A);
        }

        [Authorize(Policy = "Assistant")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Assistant")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool AppointmentExists(long id)
        {
            return _context.Appointment.Any(e => e.Id == id);
        }
    }
}
