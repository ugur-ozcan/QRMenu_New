using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace QRMenu.Infrastructure.Persistence.Repositories
{
    public class BranchRepository : BaseRepository<Branch>, IBranchRepository
    {
        public BranchRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> SlugExistsAsync(string slug, int companyId)
        {
            return await _dbSet.AnyAsync(x => x.Slug == slug && x.CompanyId == companyId && !x.IsDeleted);
        }

        public async Task<IReadOnlyList<Branch>> GetByCompanyIdAsync(int companyId)
        {
            return await _dbSet.Where(x => x.CompanyId == companyId && !x.IsDeleted).ToListAsync();
        }

        public async Task<Branch> GetBySlugAsync(string slug, string companySlug)
        {
            return await _dbSet.Include(x => x.Company)
                               .FirstOrDefaultAsync(x => x.Slug == slug &&
                                                         x.Company.Slug == companySlug &&
                                                         x.IsActive &&
                                                         !x.IsDeleted);
        }
    }
}
