using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebObserver.Main.Application.Options;
using WebObserver.Main.Application.Services.Ifaces;
using WebObserver.Main.Domain.Repositories;
using WebObserver.Main.Infrastructure.Data;
using WebObserver.Main.Infrastructure.Data.Repositories;
using WebObserver.Main.Infrastructure.Jobs.YouTubePlaylist;

namespace WebObserver.Main.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddData(configuration)
            .AddObservingApis()
            .AddHangfire(configuration);
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
        services.AddScoped<IObservingTemplateRepository, ObservingTemplateRepository>();
        services.AddScoped<IObservingRepository, ObservingRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }

    private static IServiceCollection AddObservingApis(this IServiceCollection services)
    {
        services.AddSingleton<YouTubeService>(provider =>
        {
            var options = provider.GetRequiredService<IOptionsMonitor<YouTubeOptions>>().CurrentValue;
    
            return new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = options.ApiKey,
                ApplicationName = "YouTubePlaylistFetcher"
            });
        });

        services.AddScoped<IYouTubePlaylistService, YouTubePlaylistService>();
        
        return services;
    }

    private static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(conf =>
        {
            conf.UsePostgreSqlStorage(options => 
                options.UseNpgsqlConnection(configuration.GetConnectionString("Hangfire")));
            
            conf.UseSerializerSettings(new JsonSerializerSettings() {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        });
        
        services.AddHangfireServer();

        return services;
    }
}