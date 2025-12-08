using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veluxe.Data;
using Veluxe.Models;

namespace Veluxe.Controllers
{
    public class AdminCategoryController : Controller
    {
        private readonly VeluxeDbContext _context;

        public AdminCategoryController(VeluxeDbContext context)
        {
            _context = context;
        }
        public IActionResult AdminCategory()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View("CreateCategory");
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryModel category)
        {
            if (!ModelState.IsValid) return View(category);

            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("AdminCategory");
        }

        // EDIT GET
        public IActionResult Update(string id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            return View("UpdateCategory", category);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CategoryModel category) 
        {
            if (!ModelState.IsValid) return View(category);

            _context.Categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction("AdminCategory");
        }

        // DETAILS
        public IActionResult Read(string id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            return View("ReadCategory", category);
        }

        // DELETE GET
        public IActionResult Delete(string id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            return View("DeleteCategory", category);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("AdminCategory");
        }
    }
}
