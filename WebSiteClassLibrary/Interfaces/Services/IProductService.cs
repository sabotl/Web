using WebSiteClassLibrary.Models;

namespace WebSiteClassLibrary.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<WebSiteClassLibrary.Models.Product>?> GetAllProductsAsync();
        Task<WebSiteClassLibrary.Models.Product?> GetProductByIdAsync(int id);
        Task AddProductAsync(WebSiteClassLibrary.DTO.ProductDTO product);
        Task DeleteProductAsync(int id);
        Task UpdateProductAsync(WebSiteClassLibrary.DTO.ProductDTO dto);
        Task<List<Product>?> GetByCategoryAsync(string category);
    }
}
