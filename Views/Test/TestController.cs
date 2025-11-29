using Microsoft.AspNetCore.Mvc;
using Veluxe.Data;

using Veluxe.Models;
using System.Linq;

namespace Veluxe.Views.Test
{
    public class TestController : Controller
    {
        private readonly VeluxeDbContext _context;

        public TestController(VeluxeDbContext context)
        {
            _context = context;
        }

        public IActionResult Test()
        {
            // Get all categories from database
            var categories = _context.Categories.ToList();

            // Pass to the view
            return View(categories);
        }
    }
}