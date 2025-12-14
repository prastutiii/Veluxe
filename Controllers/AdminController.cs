using Microsoft.AspNetCore.Mvc;
using Veluxe.Filters.Veluxe.Filters;
namespace Veluxe.Controllers
{
    [AdminAuthorize]
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}

