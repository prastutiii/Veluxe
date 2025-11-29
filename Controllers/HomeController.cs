using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Veluxe.Data;
using Veluxe.Models;

namespace Veluxe.Controllers
{
    public class HomeController : Controller
    {
        private readonly VeluxeDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(VeluxeDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}