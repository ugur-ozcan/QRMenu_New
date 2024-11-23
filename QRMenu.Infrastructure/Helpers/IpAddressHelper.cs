// QRMenu.Infrastructure/Helpers/IpAddressHelper.cs
using Microsoft.AspNetCore.Http;

namespace QRMenu.Infrastructure.Helpers
{
    /// <summary>
    /// Kullanıcının IP adresini almak için yardımcı sınıf.
    /// </summary>
    public static class IpAddressHelper
    {
        /// <summary>
        /// HTTP isteğinden istemcinin IP adresini döndürür.
        /// </summary>
        /// <param name="context">HttpContext örneği</param>
        /// <returns>IP adresi veya "Unknown"</returns>
        public static string GetClientIpAddress(HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        }
    }
}
