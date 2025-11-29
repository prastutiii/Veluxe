using Microsoft.AspNetCore.Mvc;
using Veluxe.Data;

namespace Veluxe.Controllers
{
    public class ProductDescController : Controller
    {
        private readonly VeluxeDbContext _context;

        public ProductDescController(VeluxeDbContext context)
        {
            _context = context;
        }
        public IActionResult ProductDesc()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
    }
}