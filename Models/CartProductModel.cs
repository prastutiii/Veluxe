using System.ComponentModel.DataAnnotations.Schema;

namespace Veluxe.Models
{
    public class CartProductModel
    {
        public int cart_id { get; set; }    
        public int product_id { get; set; }   

        public int quantity { get; set; }
        public decimal total_price { get; set; }

        [ForeignKey("cart_id")]
        public CartModel Cart { get; set; }
        [ForeignKey("product_id")]
        public ProductModel Products { get; set; }
    }
}
