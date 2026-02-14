using Microsoft.Extensions.DependencyInjection;
using MonolithTemplate.Shared.OutboxPattern;
using MonolithTemplate.Shared.Events;
using MonolithTemplate.Shared.Cqrs;

namespace MonolithTemplate.Shared;

public static class Module
{
    public static IServiceCollection AddShared(this IServiceCollection services, params System.Reflection.Assembly[] assemblies)
    {
        services.AddScoped<IOutbox, Outbox>();
        services.AddEvents(assemblies);
        services.AddCqrs(assemblies);
        return services;
    }

    private static IServiceCollection AddEvents(this IServiceCollection services, params System.Reflection.Assembly[] assemblies)
    {
        services.AddScoped<IEventBus, EventBus>();

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
