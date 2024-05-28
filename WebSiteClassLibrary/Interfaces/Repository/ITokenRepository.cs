using WebSiteClassLibrary.Models;

namespace WebSiteClassLibrary.Interfaces.Repository
{
    public interface ITokenRepository
    {
        string CreateRefreshToken();
        string CreateAccessToken(User existingUser);
        Task<(string AccessToken, string RefreshToken)> AuthorizeAsync(WebSiteClassLibrary.DTO.UserDTO user);
        Task<(string accessToken, string RefreshToken)> RefreshAsync(string accessToken, string refreshToken);
    }
}
