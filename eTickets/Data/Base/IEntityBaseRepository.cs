namespace eTickets.Data.Base;

public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T model);
    Task UpdateAsync(int id, T newModel);
    Task DeleteAsync(int id);
}