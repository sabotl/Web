using Microsoft.EntityFrameworkCore;
using WebSiteClassLibrary.Interfaces.Repository;
using WebSiteClassLibrary.Models;

namespace APIWebSite.src.Repository
{
    public class ProductRepository: BaseRepository<WebSiteClassLibrary.Models.Product>, IProductRepository
    {
        public ProductRepository(Context.MyDbContext context): base(context)
        {

        }
        public async Task<List<Product>?> GetProductsByCategoryAsync(string name)
        {
            var selectedCategory = await _context.categories.FirstOrDefaultAsync(c => c.Name == name);
            if (selectedCategory == null)
            {
                return null;
            }
            var products = await _context.products
                                 .Where(p => p.SubCategory.CategoryId == selectedCategory.Id)
                                 .ToListAsync();
            return products;
        }
    }
}
