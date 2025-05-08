using Microsoft.Extensions.DependencyInjection;
using WebObserver.Main.Application.Services.Ifaces;
using WebObserver.Main.Application.Services.Impls;

namespace WebObserver.Main.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddScoped<ITokenService, TokenService>();
        
        return services;
    }
}