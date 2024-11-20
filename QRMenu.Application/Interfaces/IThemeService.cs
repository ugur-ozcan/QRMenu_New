
using QRMenu.Application.Common;
using QRMenu.Application.DTOs;

namespace QRMenu.Application.Interfaces
{
    public interface IThemeService
    {
        Task<Result<ThemeDto>> CreateAsync(ThemeDto model);
        Task<Result<ThemeDto>> UpdateAsync(int id, ThemeDto model);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<ThemeDto>> GetByIdAsync(int id);
        Task<Result<List<ThemeDto>>> GetAllActiveAsync();
        Task<Result<bool>> ActivateAsync(int id);
        Task<Result<bool>> DeactivateAsync(int id);
    }
}
