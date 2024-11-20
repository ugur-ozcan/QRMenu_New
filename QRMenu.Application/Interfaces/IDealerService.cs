
using QRMenu.Application.Common;
using QRMenu.Application.DTOs.Dealer;

namespace QRMenu.Application.Interfaces
{
    public interface IDealerService
    {
        Task<Result<DealerDto>> CreateAsync(DealerDto model);
        Task<Result<DealerDto>> UpdateAsync(int id, DealerDto model);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<DealerDto>> GetByIdAsync(int id);
        Task<Result<PaginatedResult<DealerDto>>> GetAllAsync(int pageNumber, int pageSize, bool? isActive = null, bool? isDeleted = null);
        Task<Result<bool>> ActivateAsync(int id);
        Task<Result<bool>> DeactivateAsync(int id);
        Task<Result<bool>> ExtendLicenseAsync(int id, DateTime newExpiryDate);
    }
}
