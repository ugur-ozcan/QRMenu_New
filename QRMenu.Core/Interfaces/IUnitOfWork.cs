using QRMenu.Core.Entities;
 
namespace QRMenu.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<User> Users { get; }
        IBaseRepository<Dealer> Dealers { get; }
        IBaseRepository<Company> Companies { get; }
        IBaseRepository<Branch> Branches { get; }
        IBaseRepository<Theme> Themes { get; }
        IBaseRepository<Template> Templates { get; }
        IBaseRepository<CompanyTheme> CompanyThemes { get; }

        //Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
