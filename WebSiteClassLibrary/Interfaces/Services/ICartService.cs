namespace WebSiteClassLibrary.Interfaces.Services
{
    public interface ICartService
    {
        Task<IEnumerable<Models.CartItem>?> GetAsync(string userid);
        Task AddAsync(string userLogin, WebSiteClassLibrary.DTO.ProductToCartDTO productToCartDTO);
        Task DeleteAsync(string user, WebSiteClassLibrary.DTO.ProductToCartDTO productToCartDTO);
    }
}
