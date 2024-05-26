using WebSiteClassLibrary.Models;

namespace WebSiteClassLibrary.Interfaces.Repository
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(Guid userId);
        Task<IEnumerable<CartItem>?> GetItemsCartById(int id);
        Task CreateCartAsync(Cart cart);
    }
}
