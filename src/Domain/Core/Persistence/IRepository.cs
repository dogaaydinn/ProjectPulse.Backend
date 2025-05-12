namespace Domain.Core.Persistence;

public interface IRepository<T>
{
    Task<T?> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Delete(T entity);
}