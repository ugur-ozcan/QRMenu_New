
using QRMenu.Application.Common;
using QRMenu.Application.DTOs;

namespace QRMenu.Application.Interfaces
{
    public interface ICompanyThemeService
    {
        Task<Result<CompanyThemeDto>> CreateAsync(CompanyThemeDto model);
        Task<Result<CompanyThemeDto>> UpdateAsync(int companyId, CompanyThemeDto model);
        Task<Result<CompanyThemeDto>> GetByCompanyIdAsync(int companyId);
    }
}
