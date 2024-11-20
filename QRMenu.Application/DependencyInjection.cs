using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using FluentValidation;
using QRMenu.Application.Behaviors;
using AutoMapper;

namespace QRMenu.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // AutoMapper yapılandırması - düzeltilmiş
        services.AddAutoMapper(cfg => {
            cfg.AddMaps(assembly);
            cfg.ShouldMapProperty = p => p.GetMethod?.IsPublic == true;
        });

        // MediatR yapılandırması
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(assembly);
        });

        // FluentValidation yapılandırması
        services.AddValidatorsFromAssembly(assembly);

        // Pipeline Behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        return services;
    }
}