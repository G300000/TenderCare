using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Collections.Generic;
using Project.Data;
using Project.Models;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly TenderCareDbContext _context;

        public AccountController(TenderCareDbContext context)
        {
            _context = context;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // 1. SECRET ADMIN CHECK
            if (email == "admin@tendercare.com" && password == "admin123")
            {
                var adminClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Admin"),
                    new Claim(ClaimTypes.Role, "Admin")
                };
                await SignInUser(adminClaims);

                // Updated Message
                TempData["Success"] = "Welcome back, Admin! Accessing Dashboard...";
                return RedirectToAction("Dashboard", "Admin");
            }

            // 2. REGULAR USER CHECK
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                var userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "User")
                };

                await SignInUser(userClaims);

                // Updated Message to stop "Successfully Appointment" appearing on login
                TempData["Success"] = "Successfully logged in!";
                return RedirectToAction("Services", "Home");
            }

            ViewBag.Error = "Invalid Login credentials.";
            return View();
        }

        private async Task SignInUser(List<Claim> claims)
        {
            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var authProperties = new AuthenticationProperties { IsPersistent = true };
            await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        [HttpPost]
        public IActionResult Signup(User user, string confirmPassword)
        {
            if (user.Password != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View("Login");
            }

            if (_context.Users.Any(u => u.Email == user.Email))
            {
                ViewBag.Error = "Email is already registered.";
                return View("Login");
            }

            try
            {
                user.Role = "User"; // Ensures new accounts have the User role

                _context.Users.Add(user);
                _context.SaveChanges();

                // Specific message for account creation
                TempData["Success"] = "Account created successfully! Please log in to continue.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                var databaseError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                ViewBag.Error = "Database Error: " + databaseError;
                return View("Login");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Home");
        }
    }
}