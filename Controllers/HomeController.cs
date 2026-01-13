using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly TenderCareDbContext _context;

        public HomeController(TenderCareDbContext context) => _context = context;

        public IActionResult Index() => View();
        public IActionResult Services() => View();
        public IActionResult AboutUs() => View();

        [Authorize]
        public IActionResult MyHistory()
        {
            // 1. Get the current logged-in email
            string userEmail = User.Identity.Name;

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Index");
            }

            // 2. Search for the patient in the database
            var patient = _context.Patients
                .Include(p => p.Appointments)
                .FirstOrDefault(p => p.Email == userEmail);

            // 3. FIX: If the patient doesn't exist yet (new account), 
            // create a temporary model so the View doesn't crash.
            if (patient == null)
            {
                var newPatient = new Patient
                {
                    Email = userEmail,
                    PatientName = "New User",
                    Appointments = new List<Appointment>() // Empty list ensures .Any() works
                };
                return View(newPatient);
            }

            return View(patient);
        }

        [Authorize]
        [HttpPost]
        public IActionResult MakeAppointment(AppointmentModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = _context.Patients.FirstOrDefault(p => p.Email == model.Email)
                             ?? _context.Patients.FirstOrDefault(p => p.PatientName == model.PatientName);

                if (patient == null)
                {
                    patient = new Patient
                    {
                        PatientID = "P" + DateTime.Now.Ticks.ToString().Substring(10),
                        PatientName = model.PatientName,
                        GuardianName = model.GuardianName,
                        Email = model.Email,
                        Gender = model.Gender,
                        Address = model.Address,
                        DateOfBirth = model.DateOfBirth,
                        ContactNumber = model.ContactNumber
                    };
                    _context.Patients.Add(patient);
                    _context.SaveChanges();
                }

                var appointment = new Appointment
                {
                    AppointmentID = "A" + DateTime.Now.Ticks.ToString().Substring(10),
                    PatientID = patient.PatientID,
                    ServiceType = model.Service,
                    AppointmentDate = model.AppointmentDate,
                    Status = "Scheduled"
                };

                _context.Appointments.Add(appointment);
                _context.SaveChanges();

                // Unique key to separate appointment popups from login banners
                TempData["AppointmentBooked"] = "Success";

                return RedirectToAction("Services");
            }

            return View("Services", model);
        }
    }

    public class AppointmentModel
    {
        public string PatientName { get; set; } = string.Empty;
        public string GuardianName { get; set; } = string.Empty;
        public string Service { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? ContactNumber { get; set; }
        public string? Address { get; set; }
    }
}