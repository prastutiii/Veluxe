using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veluxe.Models
{
    public class OrderModel
    {

        [Key]
        public int order_id { get; set; }

        [Required]
        public DateTime order_date { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? district { get; set; }
        public string? address{ get; set; }
        public string? phone_number { get; set; }
        public string? status { get; set; }
        public decimal total_amount { get; set; }
        public int user_id { get; set; }

        [ForeignKey("user_id")]
        [ValidateNever]
        public UserModel? Users { get; set; }


        [ValidateNever]
        public List<OrderDetailModel> Order_Details { get; set; }
    }
}
