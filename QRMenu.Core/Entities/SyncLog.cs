
namespace QRMenu.Core.Entities
{
    public class SyncLog : BaseEntity
    {
        public int? BranchId { get; set; }
        public int CompanyId { get; set; }
        public int UpdatedCategories { get; set; }
        public int UpdatedProducts { get; set; }
        public DateTime SyncStartTime { get; set; }
        public DateTime? SyncEndTime { get; set; }
        public string Status { get; set; } // Success, Failed
        public string ErrorMessage { get; set; }
        public string Details { get; set; }
        public int FailureCount { get; set; }

        // Navigation properties
        public virtual Branch Branch { get; set; }
        public virtual Company Company { get; set; }
    }
}
