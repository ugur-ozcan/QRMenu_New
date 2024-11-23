using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRMenu.Application.Common;
using QRMenu.Application.DTOs;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Enums;
using QRMenu.Infrastructure.Services;
using QRMenu.Web.Models;

namespace QRMenu.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDealerService _dealerService;
        private readonly ICompanyService _companyService;
        private readonly IBranchService _branchService;
        private readonly IUserService _userService;
        private readonly ILogService _logService;

        public AdminController(
            ICurrentUserService currentUserService,
            IDealerService dealerService,
            ICompanyService companyService,
            IBranchService branchService,
            IUserService userService,
            ILogService logService)
        {
            _currentUserService = currentUserService;
            _dealerService = dealerService;
            _companyService = companyService;
            _branchService = branchService;
            _userService = userService;
            _logService = logService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var model = new DashboardViewModel();

            // Admin tüm istatistikleri görebilir
            if (_currentUserService.Role == UserRole.Admin)
            {
                var dealersResult = await _dealerService.GetAllAsync(1, 1000);
                var companiesResult = await _companyService.GetAllAsync(1, 1000);
                var branchesResult = await _branchService.GetAllAsync(1, 1000);
                var usersResult = await _userService.GetAllAsync(1, 1000);

                model.TotalDealers = dealersResult.Data?.TotalCount ?? 0;
                model.TotalCompanies = companiesResult.Data?.TotalCount ?? 0;
                model.TotalBranches = branchesResult.Data?.TotalCount ?? 0;
                model.TotalUsers = usersResult.Data?.TotalCount ?? 0;
            }
            // Bayi admin sadece kendi firmalarını görür
            else if (_currentUserService.Role == UserRole.DealerAdmin)
            {
                var dealerId = _currentUserService.DealerId.Value;
                var companiesResult = await _companyService.GetByDealerIdAsync(dealerId);

                model.TotalCompanies = companiesResult.Data?.Count ?? 0;
                // Bayiye ait firmaların şubelerini say
                var branchCount = 0;
                foreach (var company in companiesResult.Data)
                {
                    var branchesResult = await _branchService.GetByCompanyIdAsync(company.Id);
                    branchCount += branchesResult.Data?.Count ?? 0;
                }
                model.TotalBranches = branchCount;
            }
            // Firma admin sadece kendi şubelerini görür
            else if (_currentUserService.Role == UserRole.CompanyAdmin)
            {
                var companyId = _currentUserService.CompanyId.Value;
                var branchesResult = await _branchService.GetByCompanyIdAsync(companyId);
                model.TotalBranches = branchesResult.Data?.Count ?? 0;
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Dealers()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Dealers(int pageNumber = 1, int pageSize = 10, bool? isActive = null)
        {
            var result = await _dealerService.GetAllAsync(pageNumber, pageSize, isActive, false); // isDeleted = false
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View(new PaginatedResult<DealerDto>(new List<DealerDto>(), 0, pageNumber, pageSize));
            }

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetDealerList(int pageNumber = 1, int pageSize = 10, bool? isActive = null)
        {
            var result = await _dealerService.GetAllAsync(pageNumber, pageSize, isActive, false);
            return Json(result);
        }


        [HttpGet]
        public IActionResult Users()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _userService.GetAllAsync(pageNumber, pageSize);
            return Json(result);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Logs(DateTime? startDate = null, DateTime? endDate = null,
    string logLevel = null, int pageNumber = 1, int pageSize = 25)
        {
            var result = await _logService.GetLogsAsync(startDate, endDate, logLevel, "Admin", pageNumber, pageSize);

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Dashboard");
            }

            return View(result.Data);
        }
    }
}