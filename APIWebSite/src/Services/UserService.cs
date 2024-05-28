using APIWebSite.src.Context;
using APIWebSite.src.Repository;
using Azure.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using WebSiteClassLibrary.DTO;
using WebSiteClassLibrary.Interfaces.Repository;
using WebSiteClassLibrary.Interfaces.Services;
using WebSiteClassLibrary.Models;

namespace APIWebSite.src.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        public UserService(IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string number)
        {
            return Regex.IsMatch(number, @"^\+?\d[\d\-\(\) ]+$");
        }


        public async Task CreateAccountAsync(WebSiteClassLibrary.DTO.UserDTO user)
        {
            if (await _userRepository.UserExistsAsync(user.login))
            {
                throw new InvalidOperationException("User with the same login, email, or phone number already exists.");
            }
            var newuser = new User
            {
                login = user.login,
                Email = IsValidEmail(user.login) ? user.login : null,
                PhoneNumber = IsValidPhoneNumber(user.login) ? user.login : null,
                password = user.password,
                Role = "user",
            };
            await ((BaseRepository<User>)_userRepository).AddAsync(newuser);
        }
        public async Task<(string AccessToken, string RefreshToken)> AuthorizeAsync(WebSiteClassLibrary.DTO.UserDTO user)
        {
            return (await _tokenRepository.AuthorizeAsync(user));
        }

        public async Task<(string AccessToken, string RefreshToken)> refreshToken(TokenRequest tokenRequest)
        {
            return await _tokenRepository.RefreshAsync(tokenRequest.AccessToken, tokenRequest.RefreshToken);
        }

        public async Task<IEnumerable<WebSiteClassLibrary.Models.User>?> GetAllUsersAsync()
        {
            return await ((BaseRepository<User>)_userRepository).GetAllAsync();
        }

        public async Task<User?> GetUserByLoginAsync(string? login)
        {
            if(login == null)
                throw new Exception("Enter login for search.");

            var existingUser = await _userRepository.GetUserByLoginAsync(login);

            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }
            return existingUser;
        }
        public async Task<WebSiteClassLibrary.Models.User?> GetByIDAsync(Guid id)
        {
            return await ((BaseRepository<User>)_userRepository).GetByIDAsync(id);
        }

        public async Task UpdateAccountAsync(User user)
        {
            if (string.IsNullOrWhiteSpace(user.login) && (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrEmpty(user.PhoneNumber)))
                throw new ArgumentException("User name and email cannot be empty");

            var existingUser = await ((BaseRepository<User>)_userRepository).GetByIDAsync(user.id);
            if (existingUser == null)
                throw new KeyNotFoundException($"User with ID not found");

            await ((BaseRepository<User>)_userRepository).UpdateAsync(user);
        }
    }
}
