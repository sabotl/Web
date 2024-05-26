using Microsoft.EntityFrameworkCore;
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
        public async Task CreateCartAsync(Cart cart)
        {
            using (var context = new Context.MyDbContext())
            {
                await context.Cart.AddAsync(cart);
                await context.SaveChangesAsync();
            }
        }
    }
}
