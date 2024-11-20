using QRMenu.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Core.Specifications
{
    public class EntityListSpecification<T> : BaseSpecification<T> where T : BaseEntity
    {
        public EntityListSpecification(bool? isActive = null, bool? isDeleted = null)
        {
            if (isActive.HasValue && isDeleted.HasValue)
            {
                AddCriteria(x => x.IsActive == isActive.Value && x.IsDeleted == isDeleted.Value);
            }
            else if (isActive.HasValue)
            {
                AddCriteria(x => x.IsActive == isActive.Value);
            }
            else if (isDeleted.HasValue)
            {
                AddCriteria(x => x.IsDeleted == isDeleted.Value);
            }
        }
    }
}
