using WebSiteClassLibrary.DTO;
using WebSiteClassLibrary.Models;

namespace WebSiteClassLibrary.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateAccountAsync(DTO.UserDTO user);
        Task UpdateAccountAsync(Models.User user);
        Task<(string AccessToken, string RefreshToken)> AuthorizeAsync(DTO.UserDTO user);
        Task<(string AccessToken, string RefreshToken)> refreshToken(TokenRequest tokenRequest);
        Task<IEnumerable<Models.User>?> GetAllUsersAsync();
        Task<Models.User?> GetUserByLoginAsync(string? login);
        Task<Models.User?> GetByIDAsync(Guid id);
    }
}
