using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Project.Data;
using Project.Models;
using System.Linq;
using System;

namespace Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly TenderCareDbContext _context;

        public AdminController(TenderCareDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var appointmentsQuery = _context.Appointments
                .Include(a => a.Patient)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                appointmentsQuery = appointmentsQuery.Where(a =>
                    a.AppointmentID.ToLower().Contains(searchString) ||
                    (a.Patient != null && a.Patient.PatientName.ToLower().Contains(searchString))
                );
            }

            // SORTING: Earliest appointments first
            var appointments = appointmentsQuery
                .OrderBy(a => a.AppointmentDate)
                .ToList();

            return View(appointments);
        }

        [HttpPost]
        public IActionResult Complete(string id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                appointment.Status = "Completed";
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult Cancel(string id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                appointment.Status = "Cancelled";
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult Reschedule(string id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                appointment.Status = "Scheduled";
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        public IActionResult PatientDetails(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var patient = _context.Patients
                .Include(p => p.Appointments)
                .FirstOrDefault(p => p.PatientID == id);

            if (patient == null) return NotFound();

            return View(patient);
        }

        public IActionResult Update(string id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null) return NotFound();
            return View(appointment);
        }

        [HttpPost]
        public IActionResult Update(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Appointments.Update(appointment);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View(appointment);
        }

        // ADDED: Delete method to permanently remove an appointment
        [HttpPost]
        public IActionResult Delete(string id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }
    }
}