using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WebObserver.Main.Application.Features.Observings.Commands.AddObserving.Factories;
using WebObserver.Main.Application.Services.Ifaces;
using WebObserver.Main.Application.Services.Impls;

namespace WebObserver.Main.Application;

public static class DependencyInjection
{
    private static readonly Assembly Assembly = typeof(DependencyInjection).Assembly;
    
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(Assembly);
        });
        services.AddValidatorsFromAssembly(Assembly);
        
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IMessageFactory, MessageFactory>();
        
        services.AddScoped<IObservingFactory, TextObservingFactory>();
        services.AddScoped<IObservingFactory, YouTubePlaylistObservingFactory>();
        services.AddScoped<IObservingFactoryResolver, ObservingFactoryResolver>();
        
        return services;
    }
}