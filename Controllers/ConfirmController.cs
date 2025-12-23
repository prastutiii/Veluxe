using Microsoft.AspNetCore.Mvc;
using Veluxe.Data;

namespace Veluxe.Controllers
{
    public class ConfirmController : Controller
    {
        private readonly VeluxeDbContext _context;

        public ConfirmController(VeluxeDbContext context)
        {
            _context = context;
        }
        public IActionResult Confirm(int order_id)
        {
            var order = _context.Orders
        .FirstOrDefault(o => o.order_id == order_id);

            if (order == null)
                return RedirectToAction("Index", "Home");

            return View(order);
        }
    }
}
