using Microsoft.Extensions.Logging;
 using QRMenu.Core.Entities;
using QRMenu.Core.Enums;
using QRMenu.Core.Interfaces;

namespace QRMenu.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(ILogger<NotificationService> logger)
    {
        _logger = logger;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        _logger.LogInformation("Sending email to {To}, Subject: {Subject}", to, subject);
        // Email gönderme mantığı implemente edilecek
        await Task.CompletedTask;
    }

    public async Task SendPushNotificationAsync(int userId, string title, string message)
    {
        _logger.LogInformation("Sending push notification to User {UserId}, Title: {Title}", userId, title);
        // Push notification mantığı implemente edilecek
        await Task.CompletedTask;
    }

    public async Task SendSystemNotificationAsync(int userId, string message)
    {
        _logger.LogInformation("Sending system notification to User {UserId}: {Message}", userId, message);
        // Sistem bildirimi mantığı implemente edilecek
        await Task.CompletedTask;
    }
}