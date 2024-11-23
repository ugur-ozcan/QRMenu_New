using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRMenu.Application.Common;
using QRMenu.Application.Interfaces;
using QRMenu.Application.ViewModels;
using QRMenu.Core.Enums;
using System.Text.Json;

namespace QRMenu.Web.Controllers
{


    [Authorize]
    public class LogsController : Controller
    {
        private readonly ILogService _logService;
        private readonly ICurrentUserService _currentUserService;

        public LogsController(ILogService logService, ICurrentUserService currentUserService)
        {
            _logService = logService;
            _currentUserService = currentUserService;
        }

        public async Task<IActionResult> Index(
            DateTime? startDate = null,
            DateTime? endDate = null,
            string logLevel = null,
            string module = null,
            int pageNumber = 1)
        {
            var filter = new LogFilterModel
            {
                StartDate = startDate,
                EndDate = endDate,
                LogLevel = logLevel,
                Module = module
            };

            var result = await GetLogsBasedOnRole(filter, pageNumber);
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Export(LogFilterModel filter, string format = "excel")
        {
            var result = await _logService.ExportLogsAsync(filter, format);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var contentType = format.ToLower() == "excel"
                ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                : "text/csv";

            var fileName = $"logs-{DateTime.Now:yyyyMMddHHmmss}.{format}";

            return File(result.Data, contentType, fileName);
        }

        private async Task<Result<PaginatedResult<LogViewModel>>> GetLogsBasedOnRole(
            LogFilterModel filter, int pageNumber)
        {
            return _currentUserService.Role switch
            {
                Core.Enums.UserRole.Admin => await _logService.GetLogsAsync(
                    filter.StartDate, filter.EndDate, filter.LogLevel, filter.Module, pageNumber, 10),

                Core.Enums.UserRole.DealerAdmin => await _logService.GetDealerLogsAsync(
                    _currentUserService.DealerId.Value, filter, pageNumber, 10),

                Core.Enums.UserRole.CompanyAdmin => await _logService.GetCompanyLogsAsync(
                    _currentUserService.CompanyId.Value, filter, pageNumber, 10),

                _ => Result<PaginatedResult<LogViewModel>>.Failure("Unauthorized")
            };
        }

        //    public IActionResult ShowChanges(int logId)
        //    {
        //        var log = await _logService.GetByIdAsync(logId);
        //        if (log == null)
        //            return NotFound();

        //        if (!string.IsNullOrEmpty(log.OldValues) && !string.IsNullOrEmpty(log.NewValues))
        //        {
        //            var oldValues = JsonSerializer.Deserialize<Dictionary<string, object>>(log.OldValues);
        //            var newValues = JsonSerializer.Deserialize<Dictionary<string, object>>(log.NewValues);

        //            var changes = new List<ChangeLogViewModel>();
        //            foreach (var key in oldValues.Keys)
        //            {
        //                if (!oldValues[key].Equals(newValues[key]))
        //                {
        //                    changes.Add(new ChangeLogViewModel
        //                    {
        //                        PropertyName = key,
        //                        OldValue = oldValues[key].ToString(),
        //                        NewValue = newValues[key].ToString()
        //                    });
        //                }
        //            }

        //            return View(changes);
        //        }

        //        return View(new List<ChangeLogViewModel>());
        //    }
        //}
    }
}