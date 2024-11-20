using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Core.Interfaces
{
    public interface ILogService
    {
        Task LogInformationAsync(string module, string action, string details);
        Task LogWarningAsync(string module, string action, string details);
        Task LogErrorAsync(string module, string action, Exception exception);
    }
}
