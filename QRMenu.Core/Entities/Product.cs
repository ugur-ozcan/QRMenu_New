using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int CompanyId { get; set; }
        public int? BranchId { get; set; }
        public string ExternalId { get; set; }
        public bool IsAvailable { get; set; }
        public Dictionary<string, string> Translations { get; set; }
        public Dictionary<string, decimal> PriceHistory { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; }
        public virtual Company Company { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
