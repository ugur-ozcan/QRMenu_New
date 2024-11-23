using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Enums;

namespace QRMenu.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
                return string.IsNullOrEmpty(userIdClaim) ? (int?)null : int.Parse(userIdClaim);
            }
        }
        public string IpAddress => _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
        public string UserName => _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Unknown";

         public string Email => GetClaimValue<string>(ClaimTypes.Email);
        public UserRole? Role => GetClaimValue<UserRole>("Role");
        public int? DealerId => GetClaimValue<int>("DealerId");
        public int? CompanyId => GetClaimValue<int>("CompanyId");
        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        public List<string> Permissions => GetClaimValues("Permission");

        public bool HasPermission(string permission)
        {
            return Permissions.Contains(permission);
        }

        private T GetClaimValue<T>(string claimType)
        {
            var claim = _httpContextAccessor.HttpContext?.User?.FindFirst(claimType);
            if (claim != null && !string.IsNullOrEmpty(claim.Value))
            {
                return (T)Convert.ChangeType(claim.Value, typeof(T));
            }
            return default;
        }

        private List<string> GetClaimValues(string claimType)
        {
            return _httpContextAccessor.HttpContext?.User?.Claims
                .Where(c => c.Type == claimType)
                .Select(c => c.Value)
                .ToList() ?? new List<string>();
        }
    }
}