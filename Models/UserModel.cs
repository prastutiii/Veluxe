using System.ComponentModel.DataAnnotations;
namespace Veluxe.Models
{
    public class UserModel
    {
        [Key]
        public string user_id { get; set; }

        [Required]
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string phone { get; set; }

        public string password { get; set; }
        public string role { get; set; }

        public ICollection<OrderModel> Orders { get; set; }
    }
}
