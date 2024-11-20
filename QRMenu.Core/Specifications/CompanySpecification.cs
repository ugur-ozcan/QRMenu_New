namespace QRMenu.Core.Specifications
{
    public class CompanySpecification : BaseSpecification<Company>
    {
        public CompanySpecification(int dealerId, bool? isActive = null, bool? isDeleted = null)
            : base()
        {
            AddInclude(x => x.Branches);
            AddInclude(x => x.CompanyTheme);
            AddInclude(x => x.Location);

            if (isActive.HasValue && isDeleted.HasValue)
            {
                AddCriteria(x => x.DealerId == dealerId &&
                                x.IsActive == isActive.Value &&
                                x.IsDeleted == isDeleted.Value);
            }
            else
            {
                AddCriteria(x => x.DealerId == dealerId);
            }
        }

        public CompanySpecification(string slug)
            : base()
        {
            AddInclude(x => x.Branches);
            AddInclude(x => x.CompanyTheme);
            AddInclude(x => x.Location);
            AddCriteria(x => x.Slug == slug && x.IsActive && !x.IsDeleted);
        }
    }
}
