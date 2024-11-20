 

namespace QRMenu.Application.DTOs.Theme
{
    public class ThemeDto : BaseDto
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
    }
}
