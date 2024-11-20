using Microsoft.Extensions.DependencyInjection;
using QRMenu.Application.Interfaces;
 using System.Reflection;
using MediatR;
using FluentValidation;
using QRMenu.Core.Interfaces;

namespace QRMenu.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(assembly);
        });
        services.AddValidatorsFromAssembly(assembly);

        // Services
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IDealerService, DealerService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IThemeService, ThemeService>();
        services.AddScoped<ITemplateService, TemplateService>();

        // Infrastructure Services
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<ILogService, LogService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<IBackgroundJobService, BackgroundJobService>();

        // Repositories and UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}