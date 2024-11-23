
using QRMenu.Core.Enums;

namespace QRMenu.Application.Interfaces
{
    public interface ICurrentUserService
    {
        // Kullanıcının ID'sini alır. Eğer kullanıcı kimlik doğrulaması yapmamışsa null döner.
        int? UserId { get; }

        // Kullanıcının adını alır.
        string UserName { get; }

        // Kullanıcının e-posta adresini alır.
        string Email { get; }

        // Kullanıcının rolünü alır. Örnek: Admin, User vb.
        UserRole? Role { get; }

        // Kullanıcının bağlı olduğu bayi ID'sini alır.
        int? DealerId { get; }

        // Kullanıcının bağlı olduğu şirket ID'sini alır.
        int? CompanyId { get; }

        // Kullanıcının kimlik doğrulaması yapıp yapmadığını kontrol eder.
        bool IsAuthenticated { get; }

        // Kullanıcının sahip olduğu izinlerin listesini döner.
        List<string> Permissions { get; }

        // Kullanıcının belirli bir izne sahip olup olmadığını kontrol eder.
        bool HasPermission(string permission);

        // Kullanıcının IP adresini döner.
        string IpAddress { get; }



    }
}
