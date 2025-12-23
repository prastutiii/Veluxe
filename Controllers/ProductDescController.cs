using Microsoft.AspNetCore.Mvc;
using Veluxe.Data;
using Veluxe.Models;

namespace Veluxe.Controllers
{
    public class ProductDescController : Controller
    {
        private readonly VeluxeDbContext _context;

        public ProductDescController(VeluxeDbContext context)
        {
            _context = context;
        }
        public IActionResult ProductDesc(int productId)
        {
            var product = _context.Products
            .FirstOrDefault(p => p.product_id == productId);

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}