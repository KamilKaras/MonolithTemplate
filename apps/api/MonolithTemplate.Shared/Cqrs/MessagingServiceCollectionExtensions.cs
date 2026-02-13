using Microsoft.Extensions.DependencyInjection;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.Database.UnitOfWork;

public static class MessagingServiceCollectionExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, params System.Reflection.Assembly[] assemblies)
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
