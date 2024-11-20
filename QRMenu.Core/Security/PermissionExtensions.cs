using QRMenu.Core.Entities;
using QRMenu.Core.Enums;
 

namespace QRMenu.Core.Security
{
    public static class PermissionExtensions
    {
        public static bool HasPermission(this User user, Permission permission)
        {
            return user.Role switch
            {
                UserRole.Admin => true, // Admin her şeyi yapabilir
                UserRole.DealerAdmin => HasDealerPermission(user, permission),
                UserRole.CompanyAdmin => HasCompanyPermission(user, permission),
                _ => false
            };
        }

        private static bool HasDealerPermission(User user, Permission permission)
        {
            // Bayi sadece kendi firmalarında işlem yapabilir
            if (user.DealerId == null) return false;

            return permission.Module switch
            {
                "Company" => true, // Kendi bayisine bağlı firmalarda işlem yapabilir
                "Branch" => true,  // Kendi bayisine bağlı firmaların şubelerinde işlem yapabilir
                _ => false
            };
        }

        private static bool HasCompanyPermission(User user, Permission permission)
        {
            // Firma sadece kendi firmasında ve şubelerinde işlem yapabilir
            if (user.CompanyId == null) return false;

            return permission.Module switch
            {
                "Company" => permission.Name == "Company.Read", // Sadece okuma yapabilir
                "Branch" => true,  // Kendi şubelerinde tam yetki
                _ => false
            };
        }

    }
}
