using QRMenu.Core.Entities;
using QRMenu.Core.Specifications;

namespace QRMenu.Core.Specifications;

public class CompanyThemeSpecification : BaseSpecification<CompanyTheme>
{
    public CompanyThemeSpecification(int companyId)
        : base()
    {
        AddCriteria(x => x.CompanyId == companyId && !x.IsDeleted);
        AddInclude(x => x.Theme);
        AddInclude(x => x.Template);
    }
}