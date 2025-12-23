using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veluxe.Data;
using Veluxe.Filters.Veluxe.Filters;
using Veluxe.Models;

namespace Veluxe.Controllers
{
    [AdminAuthorize]
    public class AdminOrderController : Controller
    {
        private readonly VeluxeDbContext _context;

        public AdminOrderController(VeluxeDbContext context)
        {
            _context = context;
        }
        public IActionResult AdminOrder()
        {
             var orders = _context.Orders
            .Include(o => o.Order_Details)
            .ThenInclude(od => od.Products)
            .OrderByDescending(o => o.order_date)
            .ToList();
            return View(orders);
           
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View("CreateOrder");
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderModel order)
        {
            if (!ModelState.IsValid) return View("CreateOrder", order);

            _context.Orders.Add(order);
            _context.SaveChanges();
            return RedirectToAction("AdminOrder");
        }

        // EDIT GET
        public IActionResult Update(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return NotFound();

            return View("UpdateOrder", order);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(OrderModel order)
        {
            if (!ModelState.IsValid) return View("UpdateOrder", order);

            _context.Orders.Update(order);
            _context.SaveChanges();
            return RedirectToAction("AdminOrder");
        }

        // DETAILS
        public IActionResult Read(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return NotFound();

            return View("ReadOrder", order);
        }

        // DELETE GET
        public IActionResult Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return NotFound();

            return View("DeleteOrder", order);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return NotFound();

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("AdminOrder");
        }
    }
}
