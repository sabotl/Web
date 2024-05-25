namespace WebSiteClassLibrary.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateAccountAsync(DTO.UserDTO user);
        Task UpdateAccountAsync(Models.User user);
        Task<string> AuthorizeAsync(DTO.UserDTO user);
        Task<IEnumerable<Models.User>?> GetAllUsersAsync();
        Task<Models.User?> GetUserByLoginAsync(string? login);
        Task<Models.User?> GetByIDAsync(Guid id);
    }
}
