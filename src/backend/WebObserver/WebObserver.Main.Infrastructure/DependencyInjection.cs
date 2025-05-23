using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Npgsql;
using WebObserver.Main.Application.Options;
using WebObserver.Main.Application.Services.Ifaces;
using WebObserver.Main.Domain.Repositories;
using WebObserver.Main.Domain.Services;
using WebObserver.Main.Domain.Text;
using WebObserver.Main.Infrastructure.Data;
using WebObserver.Main.Infrastructure.Data.Repositories;
using WebObserver.Main.Infrastructure.Jobs;
using WebObserver.Main.Infrastructure.Jobs.Text;
using WebObserver.Main.Infrastructure.Jobs.YouTubePlaylist;
using WebObserver.Main.Infrastructure.Services;

namespace WebObserver.Main.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration, 
        IWebHostEnvironment environment)
    {
        return services
            .AddData(configuration)
            .AddObservingApis()
            .AddHangfire(configuration)
            .AddHttpClient()
            .AddJobServices()
            .AddNotifiers(environment);
    }

    private static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(b =>
        {
            b.UseNpgsql(new NpgsqlDataSourceBuilder(configuration.GetConnectionString("Postgres"))
                .EnableDynamicJson()
                .Build(), 
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
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
        
        
        return services;
    }

    private static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(conf =>
        {
            conf.UsePostgreSqlStorage(options => 
                options.UseNpgsqlConnection(configuration.GetConnectionString("Hangfire")));
            
            conf.UseSerializerSettings(new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        });
        
        services.AddHangfireServer();

        return services;
    }

    private static IServiceCollection AddJobServices(this IServiceCollection services)
    {
        services.AddScoped<IJobServiceFactory, TextJobServiceFactory>();
        services.AddScoped<IJobServiceFactory, YouTubePlaylistJobServiceFactory>();

        services.AddScoped<IDiffGenerator, TextDiffGenerator>();

        
        services.AddScoped<IJobServiceFactoryResolver, JobServiceFactoryResolver>();
        services.AddScoped<IObservingJobOrchestrator, ObservingJobOrchestrator>();
        
        return services;
    }

    private static IServiceCollection AddNotifiers(this IServiceCollection services, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            services.AddScoped<INotifier, FakeNotifier>();
        }
        
        return services;
    }
}