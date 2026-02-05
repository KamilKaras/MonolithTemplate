using Microsoft.Extensions.DependencyInjection;
using MonolithTemplate.Identity.Infrastructure.Database;
using MonolithTemplate.Identity.Infrastructure.IdentityModels;

namespace MonolithTemplate.Identity.Infrastructure;

public static class Module
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services)
    {
        services.AddIdentityCore();
        return services;
    }

    public static IServiceCollection AddIdentityCore(this IServiceCollection services)
    {
        services.AddIdentityCore<User>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = false;
            opt.User.RequireUniqueEmail = true;
        })
        .AddRoles<AppRole>()
        .AddEntityFrameworkStores<IdentityDbContext>();

        return services;
    }
}
