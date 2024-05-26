namespace WebSiteClassLibrary.Interfaces.Services
{
    public interface ICartService
    {
        Task<IEnumerable<Models.CartItem>?> Get(string userid);
        Task Add(string userLogin, WebSiteClassLibrary.DTO.ProductToCartDTO productToCartDTO);
        Task Delete(WebSiteClassLibrary.DTO.ProductToCartDTO productToCartDTO);
    }
}
