using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace QRMenu.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Tüm application içindeki assemblyleri al ve AutoMapper için kaydet
        var types = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(types);

        // MediatR için registration
        services.AddMediatR(x => x.RegisterServicesFromAssembly(types));

        return services;
    }
}