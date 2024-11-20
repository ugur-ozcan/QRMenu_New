using QRMenu.Core.Interfaces;
using QRMenu.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace QRMenu.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private bool _disposed;

    private IBaseRepository<User> _users;
    private IBaseRepository<Dealer> _dealers;
    private IBaseRepository<Company> _companies;
    private IBaseRepository<Branch> _branches;
    private IBaseRepository<Theme> _themes;
    private IBaseRepository<Template> _templates;
    private IBaseRepository<CompanyTheme> _companyThemes;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IBaseRepository<User> Users =>
        _users ??= new BaseRepository<User>(_context);

    public IBaseRepository<Dealer> Dealers =>
        _dealers ??= new BaseRepository<Dealer>(_context);

    public IBaseRepository<Company> Companies =>
        _companies ??= new BaseRepository<Company>(_context);

    public IBaseRepository<Branch> Branches =>
        _branches ??= new BaseRepository<Branch>(_context);

    public IBaseRepository<Theme> Themes =>
        _themes ??= new BaseRepository<Theme>(_context);

    public IBaseRepository<Template> Templates =>
        _templates ??= new BaseRepository<Template>(_context);

    public IBaseRepository<CompanyTheme> CompanyThemes =>
        _companyThemes ??= new BaseRepository<CompanyTheme>(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}