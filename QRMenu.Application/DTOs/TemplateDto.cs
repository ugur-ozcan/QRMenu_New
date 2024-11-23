using System;
using System.Linq;

namespace QRMenu.Application.DTOs
{
    public class TemplateDto : BaseDto
    {
        public string Name { get; set; }
        public string HtmlTemplate { get; set; }
        public string CssTemplate { get; set; }
    }
}
