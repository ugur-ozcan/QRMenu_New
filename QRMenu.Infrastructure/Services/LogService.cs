 using Microsoft.Extensions.Logging;
using QRMenu.Application.Common;
using QRMenu.Application.Interfaces;
using QRMenu.Application.ViewModels;

namespace QRMenu.Infrastructure.Services;

public class LogService : ILogService
{
    private readonly ILogger<LogService> _logger;

    public LogService(ILogger<LogService> logger)
    {
        _logger = logger;
    }

    public async Task LogInformationAsync(string module, string action, string details)
    {
        _logger.LogInformation("{Module} - {Action}: {Details}", module, action, details);
        await Task.CompletedTask;
    }

    public async Task LogWarningAsync(string module, string action, string details)
    {
        _logger.LogWarning("{Module} - {Action}: {Details}", module, action, details);
        await Task.CompletedTask;
    }

    public async Task LogErrorAsync(string module, string action, Exception exception)
    {
        _logger.LogError(exception, "{Module} - {Action}", module, action);
        await Task.CompletedTask;
    }

    Task ILogService.LogInformationAsync(string module, string action, string details, int? userId)
    {
        throw new NotImplementedException();
    }

    Task ILogService.LogWarningAsync(string module, string action, string details, int? userId)
    {
        throw new NotImplementedException();
    }

    Task ILogService.LogErrorAsync(string module, string action, string details, Exception exception, int? userId)
    {
        throw new NotImplementedException();
    }

    Task<Result<PaginatedResult<LogViewModel>>> ILogService.GetLogsAsync(DateTime? startDate, DateTime? endDate, string logLevel, string module, int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }
}