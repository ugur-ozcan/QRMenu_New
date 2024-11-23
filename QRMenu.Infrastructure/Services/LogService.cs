using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using QRMenu.Application.Common;
using QRMenu.Application.Interfaces;
using QRMenu.Application.ViewModels;
using QRMenu.Core.Entities;
using QRMenu.Core.Interfaces;

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

        public async Task LogInformationAsync(string module, string action, string details, int? userId = null)
        {
            _logger.LogInformation("{Module} - {Action}: {Details}", module, action, details);

            var log = new Log
            {
                Module = module,
                Action = action,
                Details = details,
                LogLevel = Core.Enums.LogLevel.Info,
                UserId = userId ?? _currentUserService.UserId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Logs.AddAsync(log);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task LogWarningAsync(string module, string action, string details, int? userId = null)
        {
            _logger.LogWarning("{Module} - {Action}: {Details}", module, action, details);

            var log = new Log
            {
                Module = module,
                Action = action,
                Details = details,
                LogLevel = Core.Enums.LogLevel.Warning,
                UserId = userId ?? _currentUserService.UserId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Logs.AddAsync(log);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task LogErrorAsync(string module, string action, string details, Exception exception = null, int? userId = null)
        {
            _logger.LogError(exception, "{Module} - {Action}: {Details}", module, action, details);

            var log = new Log
            {
                Module = module,
                Action = action,
                Details = details,
                LogLevel = Core.Enums.LogLevel.Error,
                UserId = userId ?? _currentUserService.UserId,
                Exception = exception?.ToString(),
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Logs.AddAsync(log);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Result<PaginatedResult<LogViewModel>>> GetLogsAsync(
            DateTime? startDate,
            DateTime? endDate,
            string logLevel = null,
            string module = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            try
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

                var items = query
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var logViewModels = items.Select(x => new LogViewModel
                {
                    CreatedAt = x.CreatedAt,
                    Module = x.Module,
                    Action = x.Action,
                    Details = x.Details,
                    LogLevel = x.LogLevel.ToString(),
                    UserId = x.UserId
                }).ToList();

                var paginatedResult = new PaginatedResult<LogViewModel>(logViewModels, totalCount, pageNumber, pageSize);
                return Result<PaginatedResult<LogViewModel>>.Success(paginatedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting logs");
                return Result<PaginatedResult<LogViewModel>>.Failure("Error retrieving logs");
            }
        }

        public async Task<Result<PaginatedResult<LogViewModel>>> GetDealerLogsAsync(
            int dealerId,
            LogFilterModel filter,
            int pageNumber = 1,
            int pageSize = 10)
        {
            try
            {
                var query = _unitOfWork.Logs.ListAllAsync().Result.AsQueryable()
                    .Where(x => x.User.DealerId == dealerId);

                // Filtreleri uygula
                if (filter.StartDate.HasValue)
                    query = query.Where(x => x.CreatedAt >= filter.StartDate.Value);

                if (filter.EndDate.HasValue)
                    query = query.Where(x => x.CreatedAt <= filter.EndDate.Value);

                if (!string.IsNullOrEmpty(filter.LogLevel))
                    query = query.Where(x => x.LogLevel.ToString() == filter.LogLevel);

                if (!string.IsNullOrEmpty(filter.Module))
                    query = query.Where(x => x.Module == filter.Module);

                var totalCount = query.Count();

                var items = query
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var logViewModels = items.Select(x => new LogViewModel
                {
                    CreatedAt = x.CreatedAt,
                    Module = x.Module,
                    Action = x.Action,
                    Details = x.Details,
                    LogLevel = x.LogLevel.ToString(),
                    UserId = x.UserId
                }).ToList();

                var paginatedResult = new PaginatedResult<LogViewModel>(logViewModels, totalCount, pageNumber, pageSize);
                return Result<PaginatedResult<LogViewModel>>.Success(paginatedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting dealer logs");
                return Result<PaginatedResult<LogViewModel>>.Failure("Error retrieving dealer logs");
            }
        }

        public async Task<Result<PaginatedResult<LogViewModel>>> GetCompanyLogsAsync(
    int companyId,
    LogFilterModel filter,
    int pageNumber = 1,
    int pageSize = 10)
        {
            try
            {
                var query = _unitOfWork.Logs.ListAllAsync().Result.AsQueryable()
                    .Where(x => x.User.CompanyId == companyId);

                // Filtreleri uygula
                if (filter.StartDate.HasValue)
                    query = query.Where(x => x.CreatedAt >= filter.StartDate.Value);

                if (filter.EndDate.HasValue)
                    query = query.Where(x => x.CreatedAt <= filter.EndDate.Value);

                if (!string.IsNullOrEmpty(filter.LogLevel))
                    query = query.Where(x => x.LogLevel.ToString() == filter.LogLevel);

                if (!string.IsNullOrEmpty(filter.Module))
                    query = query.Where(x => x.Module == filter.Module);

                var totalCount = query.Count();

                var items = await query
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                // LogViewModel listesine dönüştür
                var logViewModels = items.Select(x => new LogViewModel
                {
                    CreatedAt = x.CreatedAt,
                    Module = x.Module,
                    Action = x.Action,
                    Details = x.Details,
                    LogLevel = x.LogLevel.ToString(),
                    UserEmail = x.User?.Email,
                    UserName = x.User?.FullName,
                    IpAddress = x.IpAddress,
                    UserId = x.UserId,
                    CompanyId = x.User?.CompanyId,
                    DealerId = x.User?.DealerId
                }).ToList();

                // Sayfalanmış sonuç oluştur
                var paginatedResult = new PaginatedResult<LogViewModel>(
                    logViewModels,
                    totalCount,
                    pageNumber,
                    pageSize);

                // Başarılı sonucu döndür
                return Result<PaginatedResult<LogViewModel>>.Success(paginatedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting company logs for CompanyId: {CompanyId}", companyId);
                return Result<PaginatedResult<LogViewModel>>.Failure("Error retrieving company logs");
            }
        }

        public async Task<Result<PaginatedResult<LogViewModel>>> GetUserLogsAsync(
            int userId,
            LogFilterModel filter,
            int pageNumber = 1,
            int pageSize = 10)
        {
            // GetCompanyLogsAsync ile benzer implementasyon
            throw new NotImplementedException();
        }

        public async Task<Result<byte[]>> ExportLogsAsync(
            LogFilterModel filter,
            string exportFormat = "excel")
        {
            // Excel/CSV export implementasyonu
            throw new NotImplementedException();
        }

        public Task LogChangeAsync(string module, string action, string details, object oldValue, object newValue, int? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<Result<LogViewModel>> GetByIdAsync(int logId)
        {
            throw new NotImplementedException();
        }
    }
}