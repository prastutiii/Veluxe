using System.ComponentModel.DataAnnotations;
namespace Veluxe.Models
{
    public class ProductModel
    {
        [Key]
        public string product_id { get; set; }

        [Required]
        public string product_name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public string image_url { get; set; }
    }
}
