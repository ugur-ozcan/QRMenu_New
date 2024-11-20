using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Interfaces;

namespace QRMenu.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);
        services.AddMediatR(config => {
            config.RegisterServicesFromAssembly(assembly);
        });
        services.AddValidatorsFromAssembly(assembly);

        // Service kayıtları
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IDealerService, DealerService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IThemeService, ThemeService>();
        services.AddScoped<ITemplateService, TemplateService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}