using System.ComponentModel.DataAnnotations;
namespace Veluxe.Models
{
    public class CategoryModel
    {
        [Key]
        public int category_id { get; set; }

        [Required]
        public string category_name { get; set; }
        public string description { get; set; }
        public string image_url { get; set; }

        public ICollection<ProductModel> Products { get; set; }
    }
}
