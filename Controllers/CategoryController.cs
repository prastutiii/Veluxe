using Microsoft.AspNetCore.Mvc;

namespace Veluxe.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Category()
        {
            return View();
        }
    }
}