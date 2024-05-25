using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebSiteClassLibrary.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required, NotNull]
        public string Productname { get; set; }

        [Required, NotNull]
        public string Description { get; set; }

        [Required, NotNull]
        public decimal Price { get; set; }

        [Required, NotNull]
        public int Quantity { get; set; }

        [Required, ForeignKey("Shop"), NotNull]
        public int ShopId { get; set; }
        [Required, ForeignKey("Category"), NotNull]
        public int SubcategoryID { get; set; }

        public SubCategory SubCategory { get; set; }
        public Shop ProductShop { get; set; }
    }
}
