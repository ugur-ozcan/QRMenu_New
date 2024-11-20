
using QRMenu.Application.Common;
using QRMenu.Application.DTOs.Template;

namespace QRMenu.Application.Interfaces
{
    public interface ITemplateService
    {
        Task<Result<TemplateDto>> CreateAsync(TemplateDto model);
        Task<Result<TemplateDto>> UpdateAsync(int id, TemplateDto model);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<TemplateDto>> GetByIdAsync(int id);
        Task<Result<List<TemplateDto>>> GetAllActiveAsync();
        Task<Result<bool>> ActivateAsync(int id);
        Task<Result<bool>> DeactivateAsync(int id);
    }
}
