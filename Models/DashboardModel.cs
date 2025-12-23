namespace Veluxe.Models
{
    public class DashboardModel
    {
        public decimal TotalSales { get; set; }
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public int TotalUsers { get; set; }

        public List<decimal> MonthlySales { get; set; } = new List<decimal>();
        public List<string> Months { get; set; } = new List<string>();
    }
}
