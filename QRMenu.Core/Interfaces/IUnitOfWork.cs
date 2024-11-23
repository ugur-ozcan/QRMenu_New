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
        IBaseRepository<Notification> Notifications { get; } // Bunu ekledik
        IBaseRepository<Log> Logs { get; }  // Log repository'sini ekledik

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}