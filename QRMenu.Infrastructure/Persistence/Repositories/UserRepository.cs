using Microsoft.EntityFrameworkCore;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;
using QRMenu.Infrastructure.Persistence.Repositories;

namespace QRMenu.Infrastructure.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> UsernameExistsAsync(string email)
        {
            return await _dbSet.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbSet.AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IReadOnlyList<User>> GetByDealerIdAsync(int dealerId)
        {
            return await _dbSet.Where(u => u.DealerId == dealerId).ToListAsync();
        }

        public async Task<IReadOnlyList<User>> GetByCompanyIdAsync(int companyId)
        {
            return await _dbSet.Where(u => u.CompanyId == companyId).ToListAsync();
        }

        // BaseRepository'den gelen metodları override ederek User'a özel logic ekleyebiliriz
        public override async Task<User> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(u => u.Dealer)
                .Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public override async Task<IReadOnlyList<User>> ListAllAsync()
        {
            return await _dbSet
                .Include(u => u.Dealer)
                .Include(u => u.Company)
                .ToListAsync();
        }

        // Diğer BaseRepository metodları da gerekirse override edilebilir
    }
}