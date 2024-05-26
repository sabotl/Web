using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSiteClassLibrary.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        [Required]
        public Guid user_id { get; set; }
        [Required]
        public DateTime Created_at { get; set; } = DateTime.Now;

        public ICollection<CartItem> cartItems { get; set; } = new List<CartItem>();
    }
}
