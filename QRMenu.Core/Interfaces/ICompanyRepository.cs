using QRMenu.Core.Entities;

namespace QRMenu.Core.Interfaces;

public interface ICompanyRepository : IBaseRepository<Company>
{
    Task<bool> SlugExistsAsync(string slug);
    Task<IReadOnlyList<Company>> GetByDealerIdAsync(int dealerId);
    Task<Company> GetBySlugAsync(string slug);
}