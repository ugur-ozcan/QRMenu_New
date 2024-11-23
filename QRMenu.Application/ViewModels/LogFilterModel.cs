using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Application.ViewModels
{
    public class LogFilterModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string LogLevel { get; set; }
        public string Module { get; set; }
        public int? UserId { get; set; }
        public int? DealerId { get; set; }
        public int? CompanyId { get; set; }
    }
}
