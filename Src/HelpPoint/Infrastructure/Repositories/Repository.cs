using HelpPoint.Common;
using Microsoft.EntityFrameworkCore;

namespace HelpPoint.Infrastructure.Repositories;

public class Repository<T>(DbContext context) : IRepository<T>
    where T : class
{
    protected DbSet<T> Entities => context.Set<T>();

    public async Task<T?> GetByIdAsync(object id) => await Entities.FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() => await Entities.ToListAsync();

    public async Task AddAsync(T entity)
    {
        await Entities.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        Entities.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        Entities.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(object id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            await DeleteAsync(entity);
        }
    }
}
