using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eTickets.Data.Base;

public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
{
    private readonly AppDbContext _dbContext;

    public EntityBaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();
    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbContext.Set<T>();
        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().FirstOrDefaultAsync(n => n.Id == id);

    public async Task AddAsync(T model)
    {
        await _dbContext.Set<T>().AddAsync(model);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, T newModel)
    {
        EntityEntry entityEntry = _dbContext.Entry<T>(newModel);
        entityEntry.State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
        EntityEntry entityEntry = _dbContext.Entry<T>(entity);
        entityEntry.State = EntityState.Deleted;
        await _dbContext.SaveChangesAsync();
    }
}