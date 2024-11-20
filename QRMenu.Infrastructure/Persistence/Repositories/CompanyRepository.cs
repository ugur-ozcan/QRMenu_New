using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using QRMenu.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Infrastructure.Persistence.Repositories
{
    public class CompanyRepository : BaseRepository<Branch>, ICompanyRepository
    {
        public Task<Company> AddAsync(Company entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(BaseSpecification<Company> spec)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Company entity)
        {
            throw new NotImplementedException();
        }

        Task<Company> IBaseRepository<Company>.AddAsync(Company entity)
        {
            throw new NotImplementedException();
        }

        Task<int> IBaseRepository<Company>.CountAsync(BaseSpecification<Company> spec)
        {
            throw new NotImplementedException();
        }

        Task IBaseRepository<Company>.DeleteAsync(Company entity)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Company>> IBaseRepository<Company>.GetActiveAsync()
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Company>> IBaseRepository<Company>.GetAllWithStatusAsync(bool? isActive, bool? isDeleted)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Company>> ICompanyRepository.GetByDealerIdAsync(int dealerId)
        {
            throw new NotImplementedException();
        }

        Task<Company> IBaseRepository<Company>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<Company> ICompanyRepository.GetBySlugAsync(string slug)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Company>> IBaseRepository<Company>.GetDeletedAsync()
        {
            throw new NotImplementedException();
        }

        Task<Company> IBaseRepository<Company>.GetEntityWithSpec(BaseSpecification<Company> spec)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Company>> IBaseRepository<Company>.GetInactiveAsync()
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Company>> IBaseRepository<Company>.ListAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Company>> IBaseRepository<Company>.ListAsync(BaseSpecification<Company> spec)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICompanyRepository.SlugExistsAsync(string slug)
        {
            throw new NotImplementedException();
        }

        Task IBaseRepository<Company>.UpdateAsync(Company entity)
        {
            throw new NotImplementedException();
        }
    }
}
