namespace PMS.Application.Repositories;

public interface IGenericRepository<T>
{
    Task<T> CreateAsync(T entity);
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}