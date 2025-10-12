namespace HelpPoint.Common;

public interface IRepository<T> where T : class
{
    public Task<T?> GetByIdAsync(object id);
    public Task<IEnumerable<T>> GetAllAsync();
    public Task AddAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(T entity);
    public Task DeleteByIdAsync(object id);
}
