using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebSiteClassLibrary.Models
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }

        [Required, NotNull]
        public string Name { get; set; }

        [Required, NotNull]
        public string Description { get; set; }

        [Required, Timestamp, NotNull]
        public TimeSpan OpeningTime { get; set; }

        [Required, Timestamp, NotNull]
        public TimeSpan ClosingTime { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
