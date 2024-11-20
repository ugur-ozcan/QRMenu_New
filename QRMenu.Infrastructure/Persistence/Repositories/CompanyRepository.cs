using Microsoft.EntityFrameworkCore;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using QRMenu.Infrastructure.Persistence.Repositories;

namespace QRMenu.Infrastructure.Persistence.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> SlugExistsAsync(string slug)
        {
            return await _dbSet.AnyAsync(c => c.Slug == slug);
        }

        public async Task<IReadOnlyList<Company>> GetByDealerIdAsync(int dealerId)
        {
            return await _dbSet.Where(c => c.DealerId == dealerId).ToListAsync();
        }

        public async Task<Company> GetBySlugAsync(string slug)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Slug == slug);
        }

        // Aşağıdaki metodlar BaseRepository'den geliyor, ancak override edilerek
        // Company'ye özel logic eklenebilir

        public override async Task<Company> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(c => c.Dealer)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public override async Task<IReadOnlyList<Company>> ListAllAsync()
        {
            return await _dbSet
                .Include(c => c.Dealer)
                .ToListAsync();
        }

        // Diğer BaseRepository metodları da gerekirse override edilebilir
    }
}