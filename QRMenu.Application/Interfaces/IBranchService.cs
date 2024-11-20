
using QRMenu.Application.Common;
using QRMenu.Application.DTOs.Branch;

namespace QRMenu.Application.Interfaces
{

    public interface IBranchService
    {
        Task<Result<BranchDto>> CreateAsync(BranchDto model);
        Task<Result<BranchDto>> UpdateAsync(int id, BranchDto model);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<BranchDto>> GetByIdAsync(int id);
        Task<Result<BranchDto>> GetBySlugAsync(string companySlug, string branchSlug);
        Task<Result<PaginatedResult<BranchDto>>> GetAllAsync(int pageNumber, int pageSize, bool? isActive = null, bool? isDeleted = null);
        Task<Result<List<BranchDto>>> GetByCompanyIdAsync(int companyId);
        Task<Result<bool>> ActivateAsync(int id);
        Task<Result<bool>> DeactivateAsync(int id);
        Task<Result<bool>> UpdateSyncScheduleAsync(int id, string syncInterval);
    }
}
