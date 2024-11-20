using Microsoft.Extensions.Logging;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Entities;
using QRMenu.Core.Enums;
using QRMenu.Core.Interfaces;

namespace QRMenu.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public NotificationService(ILogger<NotificationService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task SendEmailAsync(string to, string subject, string body, List<string> attachments = null)
    {
        _logger.LogInformation("Sending email to {To}, Subject: {Subject}", to, subject);

        var notification = new Notification
        {
            Type = NotificationType.Email,
            Title = subject,
            Message = body,
            CreatedAt = DateTime.UtcNow,
        };

        await _unitOfWork.Notifications.AddAsync(notification);
        await _unitOfWork.SaveChangesAsync();
        await Task.CompletedTask;
    }

    public async Task SendPushNotificationAsync(int userId, string title, string message, object data = null)
    {
        _logger.LogInformation("Sending push notification to User {UserId}, Title: {Title}", userId, title);

        var notification = new Notification
        {
            Type = NotificationType.Push,
            UserId = userId,
            Title = title,
            Message = message,
            Data = data?.ToString(),
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Notifications.AddAsync(notification);
        await _unitOfWork.SaveChangesAsync();
        await Task.CompletedTask;
    }

    public async Task SendSystemNotificationAsync(int userId, string title, string message)
    {
        _logger.LogInformation("Sending system notification to User {UserId}: {Message}", userId, message);

        var notification = new Notification
        {
            Type = NotificationType.System,
            UserId = userId,
            Title = title,
            Message = message,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Notifications.AddAsync(notification);
        await _unitOfWork.SaveChangesAsync();
        await Task.CompletedTask;
    }

    public async Task<bool> MarkAsReadAsync(int notificationId)
    {
        var notification = await _unitOfWork.Notifications.GetByIdAsync(notificationId);
        if (notification == null)
            return false;

        notification.IsRead = true;
        notification.ReadAt = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<List<NotificationType>> GetUserNotificationsAsync(int userId, bool unreadOnly = false)
    {
        var specification = new NotificationSpecification(userId, unreadOnly);
        var notifications = await _unitOfWork.Notifications.ListAsync(specification);
        return notifications.Select(n => n.Type).ToList();
    }
}