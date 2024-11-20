 
namespace QRMenu.Application.Interfaces
{
    public interface IBackgroundJobService
    {
        Task ScheduleJobAsync<T>(T job, DateTime scheduledTime) where T : class;
        Task ScheduleRecurringJobAsync<T>(T job, string cronExpression) where T : class;
        Task CancelJobAsync(string jobId);
    }
}
