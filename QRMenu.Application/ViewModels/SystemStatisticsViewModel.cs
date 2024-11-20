 
namespace QRMenu.Application.ViewModels
{
    public class SystemStatisticsViewModel
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int TotalCompanies { get; set; }
        public int TotalBranches { get; set; }
        public List<LogViewModel> RecentLogs { get; set; }
        public List<PerformanceLogViewModel> SlowRequests { get; set; }
    }
}
