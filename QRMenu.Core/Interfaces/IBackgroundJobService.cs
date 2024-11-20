using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Core.Interfaces
{
    public interface IBackgroundJobService
    {
        Task ScheduleJobAsync<T>(T job, DateTime scheduledTime) where T : class;
        Task ScheduleRecurringJobAsync<T>(T job, string cronExpression) where T : class;
        Task CancelJobAsync(string jobId);
    }
}
