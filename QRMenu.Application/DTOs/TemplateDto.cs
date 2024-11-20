using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Application.DTOs
{
    public class TemplateDto : BaseDto
    {
        public string Name { get; set; }
        public string HtmlTemplate { get; set; }
        public string CssTemplate { get; set; }
    }
}
