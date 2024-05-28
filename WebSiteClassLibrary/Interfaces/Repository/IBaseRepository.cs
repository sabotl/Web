namespace WebSiteClassLibrary.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>?> GetAllAsync();
        Task AddAsync(T entiry);
        Task UpdateAsync(T entiry);
        Task DeleteAsync(T etitry);
        Task DeleteAsync(int id);
        Task DeleteAsync(Guid id);
        Task<T?> GetByIDAsync(int id);
        Task<T?> GetByIDAsync(Guid id);
        Task<bool> ExistAsync(string name);
        Task<T?> GetByNameAsync(string name);
    }
}
