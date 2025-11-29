using System.ComponentModel.DataAnnotations;
namespace Veluxe.Models
{
    public class UserModel
    {
        [Key]
        public string user_id { get; set; }

        [Required]
        public string name { get; set; }
        public string password { get; set; }
    }
}
