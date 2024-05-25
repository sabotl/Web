using WebSiteClassLibrary.DTO;
using WebSiteClassLibrary.Models;

namespace WebSiteClassLibrary.Interfaces.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>?> GetProductsByCategoryAsync(string name);
    }
}
