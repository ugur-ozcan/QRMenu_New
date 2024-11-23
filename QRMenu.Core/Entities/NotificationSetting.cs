using QRMenu.Core.Entities;

namespace QRMenu.Core.Entities
{
    /// <summary>
    /// Kullanıcıların bildirim tercihlerini belirlediği ayarları tutar.
    /// </summary>
    public class NotificationSetting : BaseEntity
    {
        public int UserId { get; set; } // Hangi kullanıcıya ait
        public bool ReceiveErrorNotifications { get; set; } = true; // Hata bildirimlerini alacak mı
        public bool ReceiveSyncErrorNotifications { get; set; } = true; // Senkronizasyon hatalarını alacak mı
        public bool ReceiveEmailNotifications { get; set; } = true; // E-posta bildirimlerini alacak mı
        public bool ReceiveWarningNotifications { get; set; } = true; // Uyarı bildirimlerini alacak mı
        public bool ReceiveInfoNotifications { get; set; } = true; // Bilgi bildirimlerini alacak mı

        // Navigation Property
        public virtual User User { get; set; } // Kullanıcı bilgisiyle ilişki
    }
}
