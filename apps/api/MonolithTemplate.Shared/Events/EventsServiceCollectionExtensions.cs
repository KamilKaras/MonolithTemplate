using Microsoft.Extensions.DependencyInjection;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.Database.UnitOfWork;
using MonolithTemplate.Shared.Events;

public static class EventsServiceCollectionExtensions
{
    public static IServiceCollection AddEvents(this IServiceCollection services, params System.Reflection.Assembly[] assemblies)
    {
        services.AddScoped<IEventBus, EventBus>();

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IIntegrationEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
