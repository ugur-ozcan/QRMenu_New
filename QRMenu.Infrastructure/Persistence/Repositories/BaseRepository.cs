using Microsoft.EntityFrameworkCore;
using QRMenu.Core.Interfaces;
using QRMenu.Core.Entities;
using System.Linq.Expressions;
using QRMenu.Core.Specifications;

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

    public virtual async Task<IReadOnlyList<T>> ListAsync(BaseSpecification<T> spec)
    {
        var query = ApplySpecification(spec);
        return await query.ToListAsync();
    }

    public virtual async Task<T> GetEntityWithSpec(BaseSpecification<T> spec)
    {
        var query = ApplySpecification(spec);
        return await query.FirstOrDefaultAsync();
    }

    public virtual async Task<int> CountAsync(BaseSpecification<T> spec)
    {
        var query = ApplySpecification(spec);
        return await query.CountAsync();
    }

    public virtual async Task<IReadOnlyList<T>> GetAllWithStatusAsync(bool? isActive, bool? isDeleted)
    {
        var query = _dbSet.AsQueryable();

        if (isActive.HasValue)
            query = query.Where(x => x.IsActive == isActive.Value);

        if (isDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == isDeleted.Value);

        return await query.ToListAsync();
    }

    private IQueryable<T> ApplySpecification(BaseSpecification<T> spec)
    {
        var query = _dbSet.AsQueryable();

        if (spec.Criteria != null)
            query = query.Where(spec.Criteria);

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        if (spec.OrderBy != null)
            query = query.OrderBy(spec.OrderBy);

        if (spec.OrderByDescending != null)
            query = query.OrderByDescending(spec.OrderByDescending);

        if (spec.IsPagingEnabled)
            query = query.Skip(spec.Skip).Take(spec.Take);

        return query;
    }

}