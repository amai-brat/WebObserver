using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebObserver.Main.Domain.Repositories;
using WebObserver.Main.Infrastructure.Data;
using WebObserver.Main.Infrastructure.Data.Repositories;

namespace WebObserver.Main.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddData(configuration);
    }

    private static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(b =>
        {
            b.UseNpgsql(configuration.GetConnectionString("Postgres"));
            b.EnableSensitiveDataLogging();
            b.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}