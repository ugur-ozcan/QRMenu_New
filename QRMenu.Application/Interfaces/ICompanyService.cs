
using QRMenu.Application.Common;
 using QRMenu.Application.DTOs;

namespace QRMenu.Application.Interfaces
{
 
    public interface ICompanyService
    {
        Task<Result<CompanyDto>> CreateAsync(CompanyDto model);
        Task<Result<CompanyDto>> UpdateAsync(int id, CompanyDto model);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<CompanyDto>> GetByIdAsync(int id);
        Task<Result<CompanyDto>> GetBySlugAsync(string slug);
        Task<Result<PaginatedResult<CompanyDto>>> GetAllAsync(int pageNumber, int pageSize, bool? isActive = null, bool? isDeleted = null);
        Task<Result<List<CompanyDto>>> GetByDealerIdAsync(int dealerId);
        Task<Result<bool>> ActivateAsync(int id);
        Task<Result<bool>> DeactivateAsync(int id);
    }

    


}
