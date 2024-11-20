using Microsoft.Extensions.DependencyInjection;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Interfaces;
using QRMenu.Infrastructure.Persistence;
using QRMenu.Infrastructure.Persistence.Repositories;
using QRMenu.Infrastructure.Services;

namespace QRMenu.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IDealerService, DealerService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IThemeService, ThemeService>();
        services.AddScoped<ITemplateService, TemplateService>();
        services.AddScoped<IQrCodeService, QrCodeService>();
        services.AddScoped<ICompanyThemeService, CompanyThemeService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
         return services;
    }
}