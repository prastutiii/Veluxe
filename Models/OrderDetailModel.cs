using System.ComponentModel.DataAnnotations.Schema;

namespace Veluxe.Models
{
    public class OrderDetailModel
    {
        public int order_id { get; set; }
        public int product_id { get; set; }

        public int quantity { get; set; }
        public decimal total_price { get; set; }

        [ForeignKey("order_id")]
        public OrderModel Orders { get; set; }

        [ForeignKey("product_id")]
        public ProductModel Products { get; set; }
    }
}
