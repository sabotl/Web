namespace WebSiteClassLibrary.Interfaces.Repository;
public interface IUserRepository
{
    Task<bool> UserExistsAsync(string login);
    Task<Models.User?> GetUserByLoginAsync(string login);
}