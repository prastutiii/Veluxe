using Microsoft.AspNetCore.Mvc;

namespace Veluxe.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult AdminProduct()
        {
            return View();
        }

        public IActionResult AdminOrder()
        {
            return View();
        }

        public IActionResult AdminUser()
        {
            return View();
        }
    }
}

