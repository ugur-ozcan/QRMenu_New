using QRMenu.Core.Entities;
namespace QRMenu.Core.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public int CompanyId { get; set; }
        public string Logo { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string CategoryView { get; set; }
        public string ProductView { get; set; }
         public string DefaultLanguage { get; set; }
        public bool ShowTableNumbers { get; set; }
        public string SyncInterval { get; set; }
        public DateTime? LastSyncDate { get; set; }

        // Navigation properties
        public virtual Company Company { get; set; }

        private List<string> _languagesSupported = new();
        public List<string> LanguagesSupported
        {
            get => _languagesSupported;
            set => _languagesSupported = value ?? new List<string>();
        }
    }

}