using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonolithTemplate.Identity.Application.Abstractions.OutboxPattern;
using MonolithTemplate.Identity.Domain.IdentityModels;
using MonolithTemplate.Identity.Infrastructure.Database.Tools;
using MonolithTemplate.Shared.Database;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Identity.Infrastructure.Database;
using MonolithTemplate.Identity.Application.Abstractions.UnitOfWork;
using MonolithTemplate.Shared.OutboxPattern;

namespace MonolithTemplate.Identity.Infrastructure;

public static class Module
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("default") ?? throw new ApplicationException("Connection string not found");

        services.AddAppDbContext<MyIdentityDbContext>(opt => opt.UseNpgsql(conn));
        services.AddIdentityDbTools();

        services.AddIdentityCore();

        services.AddCqrs([IdentityApplicationAssembly.GetAssembly]);
        services.AddEvents([IdentityApplicationAssembly.GetAssembly]);

        return services;
    }

    public static IServiceCollection AddIdentityCore(this IServiceCollection services)
    {
        services.AddIdentityCore<User>(opt =>
        {
            opt.SignIn.RequireConfirmedEmail = true;
            opt.Password.RequireNonAlphanumeric = false;
            opt.User.RequireUniqueEmail = true;
        })
        .AddRoles<AppRole>()
        .AddEntityFrameworkStores<MyIdentityDbContext>();

        return services;
    }

    private static IServiceCollection AddIdentityDbTools(this IServiceCollection services)
    {
        services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();

        services.AddScoped<IOutbox, Outbox>();

        services.AddScoped<IIdentityOutboxWriter, IdentityOutboxWriter>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(IdentityUnitOfWorkBehavior<,>));

        return services;
    }
}
