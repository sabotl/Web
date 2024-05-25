using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebSiteClassLibrary.Models
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }
        [Required, NotNull]
        public string Name { get; set; }
        [Required, ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Required, NotNull]
        public string Description { get; set; }

        public Category Category { get; set; }
    }
}
