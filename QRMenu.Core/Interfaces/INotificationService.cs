using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Core.Interfaces
{
    public interface INotificationService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendPushNotificationAsync(int userId, string title, string message);
        Task SendSystemNotificationAsync(int userId, string message);
    }
}
