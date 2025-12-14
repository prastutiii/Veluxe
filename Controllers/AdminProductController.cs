using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veluxe.Data;
using Veluxe.Filters.Veluxe.Filters;
using Veluxe.Models;

namespace Veluxe.Controllers
{
    [AdminAuthorize]
    public class AdminProductController : Controller
    {
        private readonly VeluxeDbContext _context;

        public AdminProductController(VeluxeDbContext context)
        {
            _context = context;
        }
        public IActionResult AdminProduct()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .ToList();
            return View(products);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View("CreateProduct");
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductModel product)
        {
            if (!ModelState.IsValid) return View(product);

            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("AdminProduct");
        }

        // EDIT GET
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            return View("UpdateProduct", product);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ProductModel product)
        {
            if (!ModelState.IsValid) return View(product);

            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction("AdminProduct");
        }

        // DETAILS
        public IActionResult Read(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            return View("ReadProduct", product);
        }

        // DELETE GET
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            return View("DeleteProduct", product);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("AdminProduct");
        }
    }
}