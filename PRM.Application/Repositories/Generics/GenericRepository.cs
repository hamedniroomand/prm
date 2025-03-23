using Microsoft.EntityFrameworkCore;
using PRM.Application.Database;
using PRM.Application.Models;
using Task = System.Threading.Tasks.Task;

namespace PRM.Application.Repositories;

public class GenericRepository<T>(ApplicationDbContext dbContext) : IGenericRepository<T> where T : EntityBase
{
    private readonly DbSet<T> _set = dbContext.Set<T>();

    public async Task<T> CreateAsync(T entity)
    {
        var createdEntity = await _set.AddAsync(entity);
        await SaveChanges();
        return createdEntity.Entity;
    }

    public Task<T?> GetByIdAsync(int id)
    {
        return _set.FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public Task<List<T>> GetAllAsync()
    {
        return _set.ToListAsync();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.Now;
        var updatedEntity = _set.Update(entity);
        await SaveChanges();
        return updatedEntity.Entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _set.Remove(entity);
        await SaveChanges();
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }

    public DbSet<T> GetQuery()
    {
        return _set;
    }
}