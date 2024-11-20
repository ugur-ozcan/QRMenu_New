
namespace QRMenu.Application.ViewModels
{


    public class PerformanceLogViewModel
    {
        public string Endpoint { get; set; }
        public double ResponseTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserAgent { get; set; }
        public int? StatusCode { get; set; }
    }


}