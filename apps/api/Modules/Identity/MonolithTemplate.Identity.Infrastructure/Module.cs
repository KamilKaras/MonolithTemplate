using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonolithTemplate.Identity.Infrastructure.Database;
using MonolithTemplate.Identity.Infrastructure.IdentityModels;
using MonolithTemplate.Shared.Database;

namespace MonolithTemplate.Identity.Infrastructure;

public static class Module
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration["default"] ?? throw new ApplicationException("Connection string not found");

        services.AddAppDbContext<IdentityDbContext>(opt => opt.UseNpgsql(conn));
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
