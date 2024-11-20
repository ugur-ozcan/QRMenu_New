
using QRMenu.Core.Enums;

namespace QRMenu.Application.Interfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        string UserName { get; }
        string Email { get; }
        UserRole? Role { get; }
        int? DealerId { get; }
        int? CompanyId { get; }
        bool IsAuthenticated { get; }
        List<string> Permissions { get; }
        bool HasPermission(string permission);
    }
}
