using QRMenu.Core.Entities;
using QRMenu.Core.Specifications;
using System.Linq.Expressions;
 

namespace QRMenu.Core.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(BaseSpecification<T> spec);
        Task<T> GetEntityWithSpec(BaseSpecification<T> spec);
        Task<int> CountAsync(BaseSpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        // Yeni metodlar
        Task<IReadOnlyList<T>> GetActiveAsync();
        Task<IReadOnlyList<T>> GetInactiveAsync();
        Task<IReadOnlyList<T>> GetDeletedAsync();
        Task<IReadOnlyList<T>> GetAllWithStatusAsync(bool? isActive, bool? isDeleted);
    }
}
 
