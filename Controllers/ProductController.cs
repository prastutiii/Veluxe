using Microsoft.AspNetCore.Mvc;

namespace Veluxe.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Product()
        {
            return View();
        }
    }
}
