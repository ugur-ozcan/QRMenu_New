using QRMenu.Application.Interfaces;  // Sadece bu using'i ekleyin
using Microsoft.Extensions.Logging;

namespace QRMenu.Infrastructure.Services;

public class BackgroundJobService : IBackgroundJobService
{
    private readonly ILogger<BackgroundJobService> _logger;

    public BackgroundJobService(ILogger<BackgroundJobService> logger)
    {
        _logger = logger;
    }

    public async Task ScheduleJobAsync<T>(T job, DateTime scheduledTime) where T : class
    {
        _logger.LogInformation($"Job scheduled for {scheduledTime}");
        // İş planlama mantığı
    }

    public async Task ScheduleRecurringJobAsync<T>(T job, string cronExpression) where T : class
    {
        _logger.LogInformation($"Recurring job scheduled with expression {cronExpression}");
        // Tekrarlanan iş planlama mantığı
    }

    public async Task CancelJobAsync(string jobId)
    {
        _logger.LogInformation($"Job cancelled: {jobId}");
        // İş iptal mantığı
    }
}