
using QRMenu.Core.Enums;

namespace QRMenu.Application.Interfaces
{  
public interface INotificationService
{
    Task SendEmailAsync(string to, string subject, string body, List<string> attachments = null);
    Task SendPushNotificationAsync(int userId, string title, string message, object data = null);
    Task SendSystemNotificationAsync(int userId, string title, string message);
    Task<bool> MarkAsReadAsync(int notificationId);
    Task<List<NotificationType>> GetUserNotificationsAsync(int userId, bool unreadOnly = false);
}
}
