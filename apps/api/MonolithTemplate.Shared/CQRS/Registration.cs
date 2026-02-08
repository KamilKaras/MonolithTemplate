using Microsoft.Extensions.DependencyInjection;
using MonolithTemplate.Shared.CQRS.Abstractions;
using MonolithTemplate.Shared.Database.UnitOfWork;

public static class CqrsRegistration
{
    public static IServiceCollection AddCqrs(this IServiceCollection services, params System.Reflection.Assembly[] assemblies)
    {
        services.AddScoped<IDispatcher, Dispatcher>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
