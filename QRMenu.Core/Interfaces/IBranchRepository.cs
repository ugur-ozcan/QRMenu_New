using QRMenu.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Core.Interfaces
{

    public interface IBranchRepository : IBaseRepository<Branch>
    {
        Task<bool> SlugExistsAsync(string slug, int companyId);
        Task<IReadOnlyList<Branch>> GetByCompanyIdAsync(int companyId);
        Task<Branch> GetBySlugAsync(string slug, string companySlug);
    }
}
