namespace QRMenu.Core.Entities
{
    public class CompanyTheme : BaseEntity
    {
        public int CompanyId { get; set; }
        public int ThemeId { get; set; }
        public int TemplateId { get; set; }
        public DateTime AppliedAt { get; set; }

        // Navigation properties
        public virtual Company Company { get; set; }
        public virtual Theme Theme { get; set; }
        public virtual Template Template { get; set; }
    }
}
