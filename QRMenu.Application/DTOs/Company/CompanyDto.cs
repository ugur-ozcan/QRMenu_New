

using QRMenu.Application.DTOs.Branch;

namespace QRMenu.Application.DTOs.Company
{
    public class CompanyDto : BaseDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public int DealerId { get; set; }
        public string Logo { get; set; }
        public string PhoneNumber { get; set; }
        public LocationDto Location { get; set; }
        public string CategoryView { get; set; }
        public string ProductView { get; set; }
        public bool UseBranches { get; set; }
        public bool ShowTableNumbers { get; set; }
        public List<string> LanguagesSupported { get; set; }
        public string DefaultLanguage { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
        public DateTime? LastSyncDate { get; set; }
        public List<BusinessHoursDto> BusinessHours { get; set; }
        public List<BranchDto> Branches { get; set; }
    }
}
