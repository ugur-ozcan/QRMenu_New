using QRMenu.Core.Entities;
 

namespace QRMenu.Core.Specifications
{
    public class BranchSpecification : BaseSpecification<Branch>
    {
        public BranchSpecification(int companyId, bool? isActive = null, bool? isDeleted = null)
            : base()
        {
            AddInclude(x => x.Location);

            if (isActive.HasValue && isDeleted.HasValue)
            {
                AddCriteria(x => x.CompanyId == companyId &&
                                x.IsActive == isActive.Value &&
                                x.IsDeleted == isDeleted.Value);
            }
            else
            {
                AddCriteria(x => x.CompanyId == companyId);
            }
        }

        public BranchSpecification(string companySlug, string branchSlug)
            : base()
        {
            AddInclude(x => x.Company);
            AddInclude(x => x.Location);
            AddCriteria(x => x.Company.Slug == companySlug &&
                            x.Slug == branchSlug &&
                            x.IsActive && !x.IsDeleted);
        }
    }
}
