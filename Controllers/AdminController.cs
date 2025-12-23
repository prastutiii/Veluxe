using Microsoft.AspNetCore.Mvc;
using Veluxe.Data;
using Veluxe.Models;
using Veluxe.Filters.Veluxe.Filters;
namespace Veluxe.Controllers
{
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private readonly VeluxeDbContext _context;

        public AdminController(VeluxeDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            var dashboard = new DashboardModel
            {
                TotalSales = _context.Orders.Sum(o => o.total_amount),
                TotalProducts = _context.Products.Count(),
                TotalOrders = _context.Orders.Count(),
                TotalUsers = _context.Users.Count()
            };

            var last6Months = Enumerable.Range(0, 6)
                        .Select(i => DateTime.Now.AddMonths(-i))
                        .Reverse()
                        .ToList();

            var salesByMonth = _context.Orders
            .Where(o => o.order_date.Year == DateTime.Now.Year)
            .GroupBy(o => o.order_date.Month)
            .Select(g => new { Month = g.Key, Total = g.Sum(x => x.total_amount) })
            .ToList();

            for (int month = 1; month <= 12; month++)
            {
                var sale = salesByMonth.FirstOrDefault(s => s.Month == month);
                dashboard.MonthlySales.Add(sale?.Total ?? 0);
                dashboard.Months.Add(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month));
            }


            return View(dashboard);
        }
    }
}

