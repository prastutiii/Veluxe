using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Veluxe.Data;
using Veluxe.Models;

namespace Veluxe.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly VeluxeDbContext _context;

        public RegistrationController(VeluxeDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var user = _context.Users
                    .FirstOrDefault(u => u.email == email && u.password == password);

                if (user != null)
                {
                    HttpContext.Session.SetInt32("user_id", user.user_id);
                    HttpContext.Session.SetString("user_name", user.name);
                    HttpContext.Session.SetString("user_role", user.role);

                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Append("user_name", user.name, option);

                    var role = (user.role ?? "").Trim().ToLower();
                    if (role == "admin")
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.Error = "Invalid email or password";
                }
            }
            else
            {
                ViewBag.Error = "Please fill all fields";
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();  
            return RedirectToAction("Login", "Registration");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult Register(string name, string email, string address, string phone, string password)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.email == email);
            if (existingUser != null)
            {
                ViewBag.Error = "Email already registered";
                return View();
            }

            var user = new UserModel
            {
                name = name,
                email = email,
                address = address,
                phone = phone,
                password = password,
                role = "user"
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("user_id", user.user_id);
            HttpContext.Session.SetString("user_name", user.name);
            HttpContext.Session.SetString("user_role", user.role);

            return RedirectToAction("Index", "Home");
        }
    }
}
