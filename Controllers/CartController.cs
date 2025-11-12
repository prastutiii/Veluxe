using Microsoft.AspNetCore.Mvc;

namespace Veluxe.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Cart()
        {
            return View();
        }
    }
}
