using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veluxe.Data;
using Veluxe.Models;

namespace Veluxe.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly VeluxeDbContext _context;

        public AdminUserController(VeluxeDbContext context)
        {
            _context = context;
        }
        public IActionResult AdminUser()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View("CreateUser");
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserModel user)
        {
            if (!ModelState.IsValid) return View(user);

            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("AdminUser");
        }

        // EDIT GET
        public IActionResult Update(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            return View("UpdateUser", user);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UserModel user)
        {
            if (!ModelState.IsValid) return View(user);

            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction("AdminUser");
        }

        // DETAILS
        public IActionResult Read(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            return View("ReadUser", user);
        }

        // DELETE GET
        public IActionResult Delete(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            return View("DeleteUser", user);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("AdminUser");
        }
    }
}
