using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Veluxe.Data;
using Veluxe.Models;

namespace Veluxe.Controllers
{
    public class ProfileController : Controller
    {
        private readonly VeluxeDbContext _context;

        public ProfileController(VeluxeDbContext context)
        {
            _context = context;
        }

        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetInt32("user_id");
            var userName = HttpContext.Session.GetString("user_name");

            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Registration");
            }

            var user = _context.Users
                       .Include(u => u.Orders) 
                       .FirstOrDefault(u => u.user_id == userId);
            return View(user);
        }

        // EDIT GET
        public IActionResult EditProfile()
        {
            var userId = HttpContext.Session.GetInt32("user_id");
            var user = _context.Users.Find(userId);
            if (user == null) return NotFound();

            return View("EditProfile", user);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult EditProfile(UserModel model)
        {
            var userId = HttpContext.Session.GetInt32("user_id");
            if (userId == null)
            {
                return RedirectToAction("Login"); 
            }

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Profile");
            }


            if (!string.IsNullOrEmpty(model.name))
            {
                user.name = model.name;
            }
            if (!string.IsNullOrEmpty(model.email))
            {
                user.email = model.email;
            }
            if (!string.IsNullOrEmpty(model.phone))
            {
                user.phone = model.phone;
            }
            if (!string.IsNullOrEmpty(model.address))
            {
                user.address = model.address;
            }
            if (!string.IsNullOrEmpty(model.password))
            {
                user.password = model.password;
            }

            _context.SaveChanges();

            HttpContext.Session.SetString("user_name", user.name);

            return RedirectToAction("Profile");
        }
    }
}
