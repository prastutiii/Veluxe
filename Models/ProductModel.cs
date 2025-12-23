using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Veluxe.Models
{
    public class ProductModel
    {
        [Key]
        public int product_id { get; set; }

        [Required]
        public string product_name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
        public string image_url { get; set; }
        public int category_id { get; set; }

        [ForeignKey("category_id")]
        [ValidateNever]
        public CategoryModel Category { get; set; }
    }
}
