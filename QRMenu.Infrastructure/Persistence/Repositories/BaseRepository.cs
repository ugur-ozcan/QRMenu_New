using Microsoft.EntityFrameworkCore;
using QRMenu.Core.Interfaces;
using QRMenu.Core.Entities;
using System.Linq.Expressions;

namespace QRMenu.Infrastructure.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual async Task DeleteAsync(T entity)
    {
        entity.IsDeleted = true;
        entity.IsActive = false;
        await UpdateAsync(entity);
    }

    public virtual async Task<IReadOnlyList<T>> GetActiveAsync()
    {
        return await _dbSet.Where(x => x.IsActive && !x.IsDeleted).ToListAsync();
    }

    public virtual async Task<IReadOnlyList<T>> GetInactiveAsync()
    {
        return await _dbSet.Where(x => !x.IsActive && !x.IsDeleted).ToListAsync();
    }

    public virtual async Task<IReadOnlyList<T>> GetDeletedAsync()
    {
        return await _dbSet.Where(x => x.IsDeleted).ToListAsync();
    }
}