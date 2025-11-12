using Microsoft.AspNetCore.Mvc;

namespace Veluxe.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}
