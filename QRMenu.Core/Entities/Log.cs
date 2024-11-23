using QRMenu.Core.Enums;

namespace QRMenu.Core.Entities
{
    // Core/Entities/Log.cs
    public class Log : BaseEntity
    {
        // Zorunlu alanlar (Not Null)
        public string Module { get; set; }      // Hangi modülde log oluştu (Admin, Account vb.)  
        public string Action { get; set; }      // Hangi işlem yapıldı (Login, Create, Update vb.)
        public string Details { get; set; }     // İşlem detayı
        public AppLogLevel LogLevel { get; set; }  // Bilgi, Uyarı, Hata vb.
        public string? IpAddress { get; set; }   // İşlem yapılan IP adresi
        public string UserEmail { get; set; }   // İşlemi yapan kullanıcının emaili
        public string UserRole { get; set; }    // İşlemi yapan kullanıcının rolü

        // Opsiyonel alanlar (Nullable)
        public int? UserId { get; set; }        // Kullanıcı ID (login olmadan işlem yapılabilir)
        public string? Exception { get; set; }   // Hata durumunda exception mesajı
        public string? OldValues { get; set; }   // Güncelleme öncesi değerler
        public string? NewValues { get; set; }   // Güncelleme sonrası değerler

        // Navigation property
        public virtual User? User { get; set; }
    }

}
