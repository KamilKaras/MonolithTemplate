using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonolithTemplate.Notifications.Application;
using MonolithTemplate.Shared;


namespace MonolithTemplate.Notifications.Infrastructure;

public static class Module
{
    public static IServiceCollection AddNotificationsModule(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = new[] { NotificationsInfrastructureAssembly.GetAssembly,
            NotificationsApplicationAssembly.GetAssembly
        };
        services.AddModuleShared(assemblies);

        var conn = configuration.GetConnectionString("default") ?? throw new ApplicationException("Connection string not found");

        return services;
    }




}
