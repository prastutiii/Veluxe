using Microsoft.AspNetCore.Mvc;
using Veluxe.Data;

namespace Veluxe.Controllers
{
    public class ProductController : Controller
    {
        private readonly VeluxeDbContext _context;

        public ProductController(VeluxeDbContext context)
        {
            _context = context;
        }
        public IActionResult Product()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
    }
}
