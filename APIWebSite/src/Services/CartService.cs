using APIWebSite.src.Repository;
using WebSiteClassLibrary.Models;

namespace APIWebSite.src.Services
{
    public class CartService : WebSiteClassLibrary.Interfaces.Services.ICartService
    {
        private WebSiteClassLibrary.Interfaces.Repository.ICartRepository _cartRepository;
        private WebSiteClassLibrary.Interfaces.Repository.IUserRepository _userRepository;

        public CartService(WebSiteClassLibrary.Interfaces.Repository.ICartRepository cartRepository, WebSiteClassLibrary.Interfaces.Repository.IUserRepository userRepository) { 
            _cartRepository = cartRepository;
            _userRepository = userRepository;
        }
        public async Task AddAsync(string userLogin, WebSiteClassLibrary.DTO.ProductToCartDTO productToCartDTO)
        {
            var user = await _userRepository.GetUserByLoginAsync(userLogin);
            if (user != null)
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(user.id);
                if (cart != null)
                {
                    var cartItem = new CartItem
                    {
                        CartId = cart.Id,
                        ProductId = productToCartDTO.IdProduct,
                        Quantity = productToCartDTO.AmountProduct,
                        AddedAt = DateTime.UtcNow
                    };
                    await ((BaseRepository<CartItem>)_cartRepository).AddAsync(cartItem);
                }
                else
                {
                    var newCart = new Cart
                    {
                        user_id = user.id,
                        Created_at = DateTime.Now,
                    };
                    await ((BaseRepository<Cart>)_cartRepository).AddAsync(newCart);
                }
            }
            else
                throw new Exception("User not found");
        }

        public async Task DeleteAsync(string user, WebSiteClassLibrary.DTO.ProductToCartDTO dto)
        {
            var account = await _userRepository.GetUserByLoginAsync(user);
            if (account != null)
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(account.id);
                if (cart != null)
                {
                    await _cartRepository.DeleteItemCartAsync(cart, dto);
                }
            }
        }

        public async Task<IEnumerable<CartItem>?> GetAsync(string userid)
        {
            var user = await _userRepository.GetUserByLoginAsync(userid);
            if (user != null)
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(user.id);
                if (cart != null)
                {
                    return await _cartRepository.GetItemsCartById(cart.Id);
                }
                else
                {
                    var newCart = new Cart
                    {
                        user_id = user.id,
                        Created_at = DateTime.UtcNow
                    };
                    await ((BaseRepository<Cart>)_cartRepository).AddAsync(newCart);
                }
            }
            return Enumerable.Empty<CartItem>();
        }
    }
}
