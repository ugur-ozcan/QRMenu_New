 namespace QRMenu.Web.Models
{
    public class DashboardViewModel
    {
        public int TotalDealers { get; set; }
        public int TotalCompanies { get; set; }
        public int TotalBranches { get; set; }
        public List<RecentActivity> RecentActivities { get; set; } = new();
        public string[] ActivityChartLabels { get; set; }
        public int[] ActivityChartData { get; set; }

 
        public int TotalUsers { get; set; }
    }

    public class RecentActivity
    {
        public DateTime Date { get; set; }
        public string Action { get; set; }
        public string UserName { get; set; }
    }
}