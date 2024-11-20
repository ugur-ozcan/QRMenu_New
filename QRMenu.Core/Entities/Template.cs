using QRMenu.Core.Entities;
namespace QRMenu.Core.Entities
{
    public class Template : BaseEntity
    {
        public string Name { get; set; }
        public string HtmlTemplate { get; set; }
        public string CssTemplate { get; set; }

        // Navigation properties
        public virtual ICollection<CompanyTheme> CompanyThemes { get; set; }
    }
}