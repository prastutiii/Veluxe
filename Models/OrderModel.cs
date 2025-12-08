using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veluxe.Models
{
    public class OrderModel
    {

        [Key]
        public string order_id { get; set; }

        [Required]
        public DateTime order_date { get; set; }
        public string status { get; set; }
        public double total_amount { get; set; }
        public string user_id { get; set; }

        [ForeignKey("user_id")]
        public UserModel Users { get; set; }
    }
}
