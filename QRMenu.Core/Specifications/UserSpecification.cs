using System.Linq.Expressions;
using QRMenu.Core.Entities;

namespace QRMenu.Core.Specifications;

public class UserSpecification : BaseSpecification<User>
{
    public UserSpecification(string email)
    {
        AddCriteria(x => x.Email == email && !x.IsDeleted);
    }

    public UserSpecification(bool? isActive = null, bool? isDeleted = null)
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

    public UserSpecification(int? dealerId = null, int? companyId = null)
    {
        if (dealerId.HasValue)
            AddCriteria(x => x.DealerId == dealerId.Value && !x.IsDeleted);

        if (companyId.HasValue)
            AddCriteria(x => x.CompanyId == companyId.Value && !x.IsDeleted);
    }
}