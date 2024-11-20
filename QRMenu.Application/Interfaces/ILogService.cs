
using QRMenu.Application.Common;
using QRMenu.Application.ViewModels;

namespace QRMenu.Application.Interfaces
{
    public interface ILogService
    {
        Task LogInformationAsync(string module, string action, string details, int? userId = null);
        Task LogWarningAsync(string module, string action, string details, int? userId = null);
        Task LogErrorAsync(string module, string action, string details, Exception exception = null, int? userId = null);
        Task<Result<PaginatedResult<LogViewModel>>> GetLogsAsync(
            DateTime? startDate,
            DateTime? endDate,
            string logLevel = null,
            string module = null,
            int pageNumber = 1,
            int pageSize = 10);
    }
}
