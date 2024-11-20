 

namespace QRMenu.Application.DTOs.Branch
{
    public class BranchDto : BaseDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public int CompanyId { get; set; }
        public string Logo { get; set; }
        public string PhoneNumber { get; set; }
        public LocationDto Location { get; set; }
        public string CategoryView { get; set; }
        public string ProductView { get; set; }
        public List<string> LanguagesSupported { get; set; }
        public string DefaultLanguage { get; set; }
        public bool ShowTableNumbers { get; set; }
        public string SyncInterval { get; set; }
        public DateTime? LastSyncDate { get; set; }
        public List<BusinessHoursDto> BusinessHours { get; set; }
    }
}
