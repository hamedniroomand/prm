using Microsoft.EntityFrameworkCore;
using PMS.Application.Models;
using Task = System.Threading.Tasks.Task;

namespace PMS.Application.Repositories;

public interface IGenericRepository<T> where T: EntityBase
{
    Task<T> CreateAsync(T entity);
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task SaveChanges();
    DbSet<T> GetQuery();
}