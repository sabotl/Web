using WebSiteClassLibrary.DTO;
using WebSiteClassLibrary.Models;

namespace WebSiteClassLibrary.Interfaces.Repository
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(Guid userId);
        Task<IEnumerable<CartItem>?> GetItemsCartById(int id);
        Task DeleteItemCartAsync(Cart cart, ProductToCartDTO dto);
    }
}
