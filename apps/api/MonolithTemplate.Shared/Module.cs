using Microsoft.Extensions.DependencyInjection;
using MonolithTemplate.Shared.OutboxPattern;
using MonolithTemplate.Shared.Events;
using MonolithTemplate.Shared.Cqrs;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MonolithTemplate.Shared;

public static class Module
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddScoped<IOutbox, Outbox>();
        services.AddHostedService<OutboxProcessor>();
        return services;
    }
    public static IServiceCollection AddModuleShared(this IServiceCollection services, params System.Reflection.Assembly[] assemblies)
    {
        services.AddEvents(assemblies);
        services.AddCqrs(assemblies);
        return services;
    }

    private static IServiceCollection AddEvents(this IServiceCollection services, params System.Reflection.Assembly[] assemblies)
    {
        services.TryAddSingleton<IEventBus, EventBus>();

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IIntegrationEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    private static IServiceCollection AddCqrs(this IServiceCollection services, params System.Reflection.Assembly[] assemblies)
    {
        services.AddScoped<IDispatcher, Dispatcher>();

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
