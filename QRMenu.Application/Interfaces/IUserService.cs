
using QRMenu.Application.Common;
using QRMenu.Application.DTOs;

namespace QRMenu.Application.Interfaces
{

    public interface IUserService
    {
        Task<Result<UserDto>> CreateAsync(UserDto model, string password);
        Task<Result<UserDto>> UpdateAsync(int id, UserDto model);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<UserDto>> GetByIdAsync(int id);
        Task<Result<UserDto>> GetByEmailAsync(string email);
        Task<Result<PaginatedResult<UserDto>>> GetAllAsync(int pageNumber, int pageSize, bool? isActive = null, bool? isDeleted = null);
        Task<Result<bool>> ChangePasswordAsync(int id, string currentPassword, string newPassword);
        Task<Result<bool>> ResetPasswordAsync(int id);
        Task<Result<bool>> ActivateAsync(int id);
        Task<Result<bool>> DeactivateAsync(int id);
    }
}
