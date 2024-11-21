using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using QRMenu.Application.Behaviors;

namespace QRMenu.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Register AutoMapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(assembly);
            });

            // Register MediatR with Behaviors
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            });

            // Register FluentValidation
            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}