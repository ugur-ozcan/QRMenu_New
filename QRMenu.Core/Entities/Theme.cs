using QRMenu.Core.Entities;
namespace QRMenu.Core.Entities
{
    public class Theme : BaseEntity
    {
        public string Name { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string BackgroundColor { get; set; }
        public string ButtonColor { get; set; }
        public string TitleFontFamily { get; set; }
        public string ContentFontFamily { get; set; }
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        public int FontSize { get; set; }

        // Navigation properties
        public virtual ICollection<CompanyTheme> CompanyThemes { get; set; }
    }

}