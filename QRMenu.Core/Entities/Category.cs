using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Core.Entities
{
    // Kategori ve Ürün entityleri
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int DisplayOrder { get; set; }
        public int CompanyId { get; set; }
        public int? BranchId { get; set; }
        public string ExternalId { get; set; } // Dış sistemdeki ID
        public Dictionary<string, string> Translations { get; set; } // Çoklu dil desteği için

        // Navigation properties
        public virtual Company Company { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
