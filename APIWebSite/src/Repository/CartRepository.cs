using Microsoft.EntityFrameworkCore;
using WebSiteClassLibrary.DTO;
using WebSiteClassLibrary.Models;

namespace APIWebSite.src.Repository
{
    public class CartRepository: BaseRepository<Cart>, WebSiteClassLibrary.Interfaces.Repository.ICartRepository
    {
        public CartRepository(APIWebSite.src.Context.MyDbContext context) : base(context)
        {

        }
        public async Task<Cart?> GetCartByUserIdAsync(Guid userId)
        {
            return await _context.Cart.FirstOrDefaultAsync(c => c.user_id == userId);
        }
        public async Task<IEnumerable<CartItem>?> GetItemsCartById(int id)
        {
            return await _context.CartItems
                .Where(c=> c.CartId == id)
                .ToListAsync();
        }

        public async Task DeleteItemCartAsync(Cart cart, ProductToCartDTO dto)
        {
            var cartItem = cart.cartItems.FirstOrDefault(item => item.ProductId == dto.IdProduct);
            if (cartItem == null)
            {
                throw new ArgumentException("Product not found in cart");
            }
            if(cartItem.Quantity > dto.AmountProduct)
            {
                cartItem.Quantity -= dto.AmountProduct;
            }
            else
            {
                cart.cartItems.Remove(cartItem);
            }

            await _context.SaveChangesAsync();
        }
    }
}
