using Microsoft.EntityFrameworkCore;
using PMS.Application.Database;
using PMS.Application.Models;

namespace PMS.Application.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<T> _set;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _set = dbContext.Set<T>();
    }

    public async Task<T> CreateAsync(T entity)
    {
        var createdEntity = await _set.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
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
        await _dbContext.SaveChangesAsync();
        return updatedEntity.Entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _set.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}