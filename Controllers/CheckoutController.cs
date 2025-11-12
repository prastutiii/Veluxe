using Microsoft.AspNetCore.Mvc;

namespace Veluxe.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
