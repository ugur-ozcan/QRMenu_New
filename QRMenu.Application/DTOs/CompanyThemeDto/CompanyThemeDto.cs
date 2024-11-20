using QRMenu.Application.DTOs.Template;
using QRMenu.Application.DTOs.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Application.DTOs.CompanyThemeDto
{
    public class CompanyThemeDto : BaseDto
    {
        public int CompanyId { get; set; }
        public int ThemeId { get; set; }
        public int TemplateId { get; set; }
        public DateTime AppliedAt { get; set; }
        public ThemeDto Theme { get; set; }
        public TemplateDto Template { get; set; }
    }
}
