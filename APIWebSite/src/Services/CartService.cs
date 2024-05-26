using APIWebSite.src.Repository;
using WebSiteClassLibrary.DTO;
using WebSiteClassLibrary.Interfaces.Services;
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
        public async Task Add(string userLogin, WebSiteClassLibrary.DTO.ProductToCartDTO productToCartDTO)
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

        public Task Delete(WebSiteClassLibrary.DTO.ProductToCartDTO p)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CartItem>?> Get(string userid)
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
                    await _cartRepository.CreateCartAsync(newCart);
                }
            }
            return Enumerable.Empty<CartItem>();
        }
    }
}
