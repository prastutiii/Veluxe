using Microsoft.AspNetCore.Mvc;

namespace Veluxe.Controllers
{
    public class ProductDescController : Controller
    {
        public IActionResult ProductDesc()
        {
            return View();
        }
    }
}