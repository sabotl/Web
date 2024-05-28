using Microsoft.EntityFrameworkCore;
using WebSiteClassLibrary.Interfaces.Repository;

namespace APIWebSite.src.Repository
{
    public class BaseRepository<T>: IBaseRepository<T> where T : class
    {
        protected readonly Context.MyDbContext _context;
        public BaseRepository(Context.MyDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entiry)
        {
            await _context.Set<T>().AddAsync(entiry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if(entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(T entry)
        {
            if (entry != null)
            {
                _context.Set<T>().Remove(entry);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIDAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIDAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistAsync(string name)
        {
            var entity = await _context.Set<T>().FindAsync(name);
            if(entity != null)
                return true;
            return false;
        }
        public async Task<T?> GetByNameAsync(string name)
        {
            var entity = await _context.Set<T>().FindAsync(name);
            if (entity != null)
                return entity;
            return null;
        }
    }
}
