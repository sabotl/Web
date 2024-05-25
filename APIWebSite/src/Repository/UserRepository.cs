using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebSiteClassLibrary.Interfaces.Repository;
using WebSiteClassLibrary.Models;

namespace APIWebSite.src.Repository
{
    public class UserRepository : BaseRepository<WebSiteClassLibrary.Models.User>, IUserRepository
    {
        public UserRepository(APIWebSite.src.Context.MyDbContext context): base(context) {

        }
        public async Task<WebSiteClassLibrary.Models.User?> GetUserByLoginAsync(string login)
        {
            return await _context.users
                            .FirstOrDefaultAsync(u => u.login == login || u.Email == login || u.PhoneNumber == login);
        }
        public async Task<bool> UserExistsAsync(string login)
        {
            return await _context.users.AnyAsync(u => u.login == login || u.Email == login || u.PhoneNumber == login);
        }
    }
}
