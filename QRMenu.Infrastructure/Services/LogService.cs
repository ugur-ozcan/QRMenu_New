using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QRMenu.Core.Entities;
using QRMenu.Core.Enums;
using QRMenu.Core.Interfaces;
using QRMenu.Application.Common;
using QRMenu.Application.ViewModels;
using QRMenu.Application.Interfaces;

namespace QRMenu.Infrastructure.Services
{
    public class LogService : ILogService
    {
        private readonly ILogger<LogService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public LogService(
            ILogger<LogService> logger,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        #region Log Methods

        public async Task LogInformationAsync(string module, string action, string details, int? userId = null)
        {
            await LogAsync(AppLogLevel.Info, module, action, details, null, userId);
        }

        public async Task LogWarningAsync(string module, string action, string details, int? userId = null)
        {
            await LogAsync(AppLogLevel.Warning, module, action, details, null, userId);
        }

        public async Task LogErrorAsync(string module, string action, string details, Exception exception = null, int? userId = null)
        {
            await LogAsync(AppLogLevel.Error, module, action, details, exception, userId);
        }

        private async Task LogAsync(AppLogLevel logLevel, string module, string action, string details, Exception exception, int? userId)
        {
            var log = new Log
            {
                Module = module,
                Action = action,
                Details = details,
                LogLevel = logLevel,
                Exception = exception?.ToString(),
                UserId = userId ?? _currentUserService.UserId,
                UserEmail = _currentUserService.Email ?? "Unknown",
                UserRole = _currentUserService.Role?.ToString() ?? "Unknown",
                IpAddress = _currentUserService.IpAddress ?? "Unknown",
                CreatedAt = DateTime.UtcNow,
                User = userId.HasValue ? await _unitOfWork.Users.GetByIdAsync(userId.Value) : null
            };

            await _unitOfWork.Logs.AddAsync(log);
            await _unitOfWork.SaveChangesAsync();
        }

        #endregion

        #region Advanced Log Methods

        public async Task LogChangeAsync(string module, string action, string details, object oldValue, object newValue, int? userId = null)
        {
            var log = new Log
            {
                Module = module,
                Action = action,
                Details = details,
                OldValues = oldValue?.ToString(),
                NewValues = newValue?.ToString(),
                LogLevel = AppLogLevel.Info,
                UserId = userId ?? _currentUserService.UserId,
                UserEmail = _currentUserService.Email ?? "Unknown",
                UserRole = _currentUserService.Role?.ToString() ?? "Unknown",
                IpAddress = _currentUserService.IpAddress ?? "Unknown",
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Logs.AddAsync(log);
            await _unitOfWork.SaveChangesAsync();
        }

        #endregion

        #region Log Retrieval Methods

        public async Task<Result<LogViewModel>> GetByIdAsync(int logId)
        {
            var log = await _unitOfWork.Logs.GetByIdAsync(logId);
            if (log == null)
                return Result<LogViewModel>.Failure("Log not found.");

            var logViewModel = new LogViewModel
            {
                Module = log.Module,
                Action = log.Action,
                Details = log.Details,
                CreatedAt = log.CreatedAt,
                LogLevel = log.LogLevel.ToString(),
                UserId = log.UserId,
                UserEmail = log.UserEmail,
                UserRole = log.UserRole
            };

            return Result<LogViewModel>.Success(logViewModel);
        }

        public async Task<Result<PaginatedResult<LogViewModel>>> GetLogsAsync(DateTime? startDate, DateTime? endDate, string logLevel, string module, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Logs.ListAllAsync().Result.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(x => x.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.CreatedAt <= endDate.Value);

            if (!string.IsNullOrEmpty(logLevel))
                query = query.Where(x => x.LogLevel.ToString() == logLevel);

            if (!string.IsNullOrEmpty(module))
                query = query.Where(x => x.Module == module);

            var totalCount = query.Count();
            var logs = query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var logViewModels = logs.Select(x => new LogViewModel
            {
                Module = x.Module,
                Action = x.Action,
                Details = x.Details,
                CreatedAt = x.CreatedAt,
                LogLevel = x.LogLevel.ToString(),
                UserId = x.UserId,
                UserEmail = x.UserEmail,
                UserRole = x.UserRole
            }).ToList();

            var paginatedResult = new PaginatedResult<LogViewModel>(logViewModels, totalCount, pageNumber, pageSize);
            return Result<PaginatedResult<LogViewModel>>.Success(paginatedResult);
        }

        public async Task<Result<PaginatedResult<LogViewModel>>> GetDealerLogsAsync(int dealerId, LogFilterModel filter, int pageNumber, int pageSize)
        {
            var logs = await _unitOfWork.Logs.ListAllAsync();
            var dealerLogs = logs.Where(log => log.User?.DealerId == dealerId).ToList();
            return PaginateLogs(dealerLogs, pageNumber, pageSize);
        }

        public async Task<Result<PaginatedResult<LogViewModel>>> GetCompanyLogsAsync(int companyId, LogFilterModel filter, int pageNumber, int pageSize)
        {
            var logs = await _unitOfWork.Logs.ListAllAsync();
            var companyLogs = logs.Where(log => log.User?.CompanyId == companyId).ToList();
            return PaginateLogs(companyLogs, pageNumber, pageSize);
        }

        public async Task<Result<PaginatedResult<LogViewModel>>> GetUserLogsAsync(int userId, LogFilterModel filter, int pageNumber, int pageSize)
        {
            var logs = await _unitOfWork.Logs.ListAllAsync();
            var userLogs = logs.Where(log => log.UserId == userId).ToList();
            return PaginateLogs(userLogs, pageNumber, pageSize);
        }

        public async Task<Result<byte[]>> ExportLogsAsync(LogFilterModel filter, string exportFormat)
        {
            // Dışa aktarma işlemleri henüz uygulanmadı.
            throw new NotImplementedException("Export logic not implemented.");
        }

        #endregion

        #region Helper Methods

        private Result<PaginatedResult<LogViewModel>> PaginateLogs(List<Log> logs, int pageNumber, int pageSize)
        {
            var totalCount = logs.Count;
            var paginatedLogs = logs
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(log => new LogViewModel
                {
                    Module = log.Module,
                    Action = log.Action,
                    Details = log.Details,
                    CreatedAt = log.CreatedAt,
                    LogLevel = log.LogLevel.ToString(),
                    UserId = log.UserId,
                    UserEmail = log.UserEmail,
                    UserRole = log.UserRole
                })
                .ToList();

            return Result<PaginatedResult<LogViewModel>>.Success(new PaginatedResult<LogViewModel>(paginatedLogs, totalCount, pageNumber, pageSize));
        }

        #endregion
    }
}
