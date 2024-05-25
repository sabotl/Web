using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebSiteClassLibrary.DTO
{
    public class ProductDTO
    {
        public int id { get; set; }
        public string Productname { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ShopId { get; set; }
        public int SubcategoryID { get; set; }
    }
}
