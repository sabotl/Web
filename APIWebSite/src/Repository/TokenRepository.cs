using APIWebSite.src.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using WebSiteClassLibrary.Interfaces.Repository;
using WebSiteClassLibrary.Models;

namespace APIWebSite.src.Repository
{
    public class TokenRepository: BaseRepository<RefreshToken>, WebSiteClassLibrary.Interfaces.Repository.ITokenRepository
    {
        private readonly IUserRepository _userRepository;
        public TokenRepository(IUserRepository userRepository, Context.MyDbContext context): base(context) { 
            _userRepository = userRepository;
        }
        public string CreateAccessToken(User existingUser)
        {
            var creds = new SigningCredentials(Context.AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, existingUser.login),
                new Claim(ClaimTypes.Role, existingUser.Role),
            };

            var token = new JwtSecurityToken(
                issuer: Context.AuthOptions.ISSUER,
                audience: Context.AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            var randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
        public async Task<(string AccessToken, string RefreshToken)> AuthorizeAsync(WebSiteClassLibrary.DTO.UserDTO user)
        {
            var existingUser = await _userRepository.GetUserByLoginAsync(user.login);
            if (existingUser == null)
            {
                throw new UnauthorizedAccessException("User not found.");
            }
            if (existingUser.password != user.password)
            {
                throw new UnauthorizedAccessException("Invalid password.");
            }

            var existingRefreshToken = await GetTokenByUserIdAsync(existingUser.id);
            if (existingRefreshToken != null)
            {
                return (CreateAccessToken(existingUser), existingRefreshToken.Token);
            }

            var accessToken = CreateAccessToken(existingUser);
            var refreshToken = CreateRefreshToken();

            await AddAsync(new RefreshToken
            {
                Token = refreshToken,
                UserId = existingUser.id,
                ExpiryDate = DateTime.UtcNow.AddDays(30),
            });

            return (accessToken, refreshToken);
        }
        public async Task<(string accessToken, string RefreshToken)> RefreshAsync(string accessToken, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(accessToken);
            var userLogin = principal.FindFirst(ClaimTypes.Name)?.Value;
            var user = await _userRepository.GetUserByLoginAsync(userLogin);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }

            var savedRefreshToken = await GetTokenByNameAsync(refreshToken);
            if (savedRefreshToken == null || savedRefreshToken.UserId != user.id || savedRefreshToken.ExpiryDate <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }

            var newAccessToken = CreateAccessToken(user);
            var newRefreshToken = CreateRefreshToken();

            await DeleteAsync(savedRefreshToken);
            await AddAsync(new RefreshToken
            {
                Token = newRefreshToken,
                UserId = user.id,
                ExpiryDate = DateTime.UtcNow.AddDays(30)
            });
            return (newAccessToken, newRefreshToken);
        }

        public async Task<RefreshToken?> GetTokenByNameAsync(string token)
        {
            return await _context.RefreshToken.FirstOrDefaultAsync(e => e.Token == token);
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateLifetime = false
            }, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtToken || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
        private async Task<RefreshToken?> GetTokenByUserIdAsync(Guid userId)
        {
            return await _context.RefreshToken.FirstOrDefaultAsync(rt => rt.UserId == userId);
        }
    }
}
