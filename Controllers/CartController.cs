using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Veluxe.Data;
using Veluxe.Models;

namespace Veluxe.Controllers
{
    public class CartController : Controller
    {
        private readonly VeluxeDbContext _context;

        public CartController(VeluxeDbContext context)
        {
            _context = context;
        }

        public IActionResult Cart()
        {
            var userId = HttpContext.Session.GetInt32("user_id");
            var userName = HttpContext.Session.GetString("user_name");

            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Registration");
            }

            ViewBag.UserName = userName;

            var cart = _context.Cart
                .Include(c => c.Cart_Products)
                .ThenInclude(cp => cp.Products)
                .FirstOrDefault(c => c.user_id == userId.Value);

            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
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
                    user_id = userId.Value,
                    count = 0,
                    grand_total = 0,
                    Cart_Products = new List<CartProductModel>()
                };
                _context.Cart.Add(cart);
                _context.SaveChanges();
            }

            var product = _context.Products.Find(productId);
            if (product == null)
                return NotFound();

            var cartItem = cart.Cart_Products.FirstOrDefault(cp => cp.product_id == productId);
            if (cartItem != null)
            {
                cartItem.quantity += 1;
                cartItem.total_price = cartItem.quantity * product.price;
            }
            else
            {
                cartItem = new CartProductModel
                {
                    cart_id = cart.cart_id,
                    product_id = productId,
                    quantity = 1,
                    total_price = product.price
                };
                cart.Cart_Products.Add(cartItem);
            }

            cart.count = cart.Cart_Products.Sum(cp => cp.quantity);
            cart.grand_total = cart.Cart_Products.Sum(cp => cp.total_price);

            _context.SaveChanges();

            return RedirectToAction("ProductDesc", "ProductDesc", new { productId = productId });
        }


        public IActionResult Remove(int productId)
        {
            var userId = HttpContext.Session.GetInt32("user_id");
            if (!userId.HasValue) return RedirectToAction("Login", "Registration");

            var cart = _context.Cart
                .Include(c => c.Cart_Products)
                .FirstOrDefault(c => c.user_id == userId.Value);

            var item = cart?.Cart_Products.FirstOrDefault(cp => cp.product_id == productId);
            if (item != null)
            {
                cart.Cart_Products.Remove(item);
                _context.Cart_Products.Remove(item);

                cart.count = cart.Cart_Products.Sum(cp => cp.quantity);
                cart.grand_total = cart.Cart_Products?.Sum(cp => cp.total_price) ?? 0;

                _context.SaveChanges();
            }

            return RedirectToAction("Cart");
        }

        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var userId = HttpContext.Session.GetInt32("user_id");
            if (!userId.HasValue) return RedirectToAction("Login", "Registration");

            var cart = _context.Cart
                .Include(c => c.Cart_Products)
                .ThenInclude(cp => cp.Products)
                .FirstOrDefault(c => c.user_id == userId.Value);

            if (cart == null) return RedirectToAction("Cart");

            var cartItem = cart.Cart_Products.FirstOrDefault(cp => cp.product_id == productId);
            if (cartItem != null && quantity > 0)
            {
                cartItem.quantity = quantity;
                cartItem.total_price = cartItem.Products.price * quantity;

                cart.count = cart.Cart_Products.Sum(cp => cp.quantity);
                cart.grand_total = cart.Cart_Products.Sum(cp => cp.total_price);

                _context.SaveChanges();
            }

            return RedirectToAction("Cart");
        }
    }
}
