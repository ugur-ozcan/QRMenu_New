using QRMenu.Application.Common;
using QRMenu.Application.ViewModels;

namespace QRMenu.Application.Interfaces
{
    public interface ILogService
    {
        // Log Kaydetme Metodları
        Task LogInformationAsync(string module, string action, string details, int? userId = null);
        Task LogWarningAsync(string module, string action, string details, int? userId = null);
        Task LogErrorAsync(string module, string action, string details, Exception exception = null, int? userId = null);

 
        Task<Result<PaginatedResult<LogViewModel>>> GetLogsAsync(DateTime? startDate, DateTime? endDate, string logLevel, string module, int pageNumber, int pageSize);

        // Yeni metodlar ekleyelim
        Task LogChangeAsync(string module, string action, string details, object oldValue, object newValue, int? userId = null);
        Task<Result<LogViewModel>> GetByIdAsync(int logId);


      

        // Role Bazlı Log Sorgulama
        Task<Result<PaginatedResult<LogViewModel>>> GetDealerLogsAsync(
            int dealerId,
            LogFilterModel filter,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Result<PaginatedResult<LogViewModel>>> GetCompanyLogsAsync(
            int companyId,
            LogFilterModel filter,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Result<PaginatedResult<LogViewModel>>> GetUserLogsAsync(
            int userId,
            LogFilterModel filter,
            int pageNumber = 1,
            int pageSize = 10);

        // Export Metodları
        Task<Result<byte[]>> ExportLogsAsync(
            LogFilterModel filter,
            string exportFormat = "excel");
    }
}