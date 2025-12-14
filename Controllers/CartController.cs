using Microsoft.AspNetCore.Mvc;
using Veluxe.Data;
using Veluxe.Models;

namespace Veluxe.Controllers
{
    public class CartController : Controller
    {
        private readonly VeluxeDbContext _context;

        public CartController(VeluxeDbContext context)
        {
            _context = context;
        }

        public IActionResult Cart()
        {
            var userId = HttpContext.Session.GetInt32("user_id");
            var userName = HttpContext.Session.GetString("user_name");

            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Registration");
            }

            ViewBag.UserName = userName;
            var user = _context.Users.FirstOrDefault(u => u.user_id == userId);
            return View(user);
        }
    }
}
