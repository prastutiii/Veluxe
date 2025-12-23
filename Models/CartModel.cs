using System.ComponentModel.DataAnnotations;

namespace Veluxe.Models
{
    public class CartModel
    {
        [Key]
        public int cart_id { get; set; }

        public int user_id { get; set; }
        public int count { get; set; }
        public decimal grand_total { get; set; }

        public List<CartProductModel> Cart_Products { get; set; }
    }
}
