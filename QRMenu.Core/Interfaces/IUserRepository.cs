using QRMenu.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Core.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);
        Task<User> GetByEmailAsync(string email);
        Task<IReadOnlyList<User>> GetByDealerIdAsync(int dealerId);
        Task<IReadOnlyList<User>> GetByCompanyIdAsync(int companyId);
    }
}
