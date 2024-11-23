using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Interfaces;
using QRMenu.Infrastructure.Persistence;
using QRMenu.Infrastructure.Persistence.Repositories;
using QRMenu.Infrastructure.Services;
 

namespace QRMenu.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Add HttpContextAccessor
        services.AddHttpContextAccessor();
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

        // Core Services
         services.AddScoped<ICurrentUserService, CurrentUserService>();

        // Repositories
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Application Services
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IDealerService, DealerService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IThemeService, ThemeService>();
        services.AddScoped<ITemplateService, TemplateService>();
        services.AddScoped<ICompanyThemeService, CompanyThemeService>();

        // Infrastructure Services
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<ILogService, LogService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<IBackgroundJobService, BackgroundJobService>();
        services.AddScoped<IQRCodeService, QRCodeService>();

        return services;
    }
}