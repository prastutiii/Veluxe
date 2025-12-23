using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veluxe.Data;
using Veluxe.Models;

namespace Veluxe.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly VeluxeDbContext _context;

        public CheckoutController(VeluxeDbContext context)
        {
            _context = context;
        }

        public IActionResult Checkout()
        {
            var userId = HttpContext.Session.GetInt32("user_id");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Registration");

            var cart = _context.Cart
                .Include(c => c.Cart_Products)
                .ThenInclude(cp => cp.Products) 
                .FirstOrDefault(c => c.user_id == userId.Value);

            if (cart == null)
            {
                cart = new CartModel
                {
                    Cart_Products = new List<CartProductModel>(),
                    grand_total = 0
                };
            }

            var viewModel = new CheckoutModel
            {
                Cart = cart,
                Orders = new OrderModel()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult PlaceOrder(OrderModel order)
        {
            var userId = HttpContext.Session.GetInt32("user_id");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Registration");

            var cart = _context.Cart
                .Include(c => c.Cart_Products)
                .ThenInclude(cp => cp.Products)
                .FirstOrDefault(c => c.user_id == userId.Value);

            if (cart == null || !cart.Cart_Products.Any())
                return RedirectToAction("Cart", "Cart");

            order.user_id = userId.Value;
            order.order_date = DateTime.Now;
            order.status = "Pending";
            order.total_amount = cart.grand_total;

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var cp in cart.Cart_Products)
            {
                var orderDetail = new OrderDetailModel
                {
                    order_id = order.order_id,
                    product_id = cp.product_id,
                    quantity = cp.quantity,
                    total_price = cp.total_price
                };
                _context.Order_Details.Add(orderDetail);

                var product = _context.Products
                        .FirstOrDefault(p => p.product_id == cp.product_id);

                if (product != null)
                {
                    product.stock -= cp.quantity;

                    if (product.stock < 0)
                        product.stock = 0;
                }
            }

            _context.Cart_Products.RemoveRange(cart.Cart_Products);
            cart.Cart_Products.Clear();
            cart.grand_total = 0;
            cart.count = 0;

            _context.SaveChanges();

            return RedirectToAction("Confirm", "Confirm");
        }
    }
}
