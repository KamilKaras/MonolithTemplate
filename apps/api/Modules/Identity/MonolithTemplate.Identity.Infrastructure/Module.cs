using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonolithTemplate.Identity.Application.Abstractions.OutboxPattern;
using MonolithTemplate.Identity.Application;
using MonolithTemplate.Identity.Domain.IdentityModels;
using MonolithTemplate.Identity.Infrastructure.Auth;
using MonolithTemplate.Identity.Infrastructure.Database.Tools;
using MonolithTemplate.Shared.Database;
using MonolithTemplate.Shared.Cqrs;
using MonolithTemplate.Shared.OutboxPattern;
using MonolithTemplate.Shared.Events;
using MonolithTemplate.Identity.Application.Abstractions.UnitOfWork;
using MonolithTemplate.Shared;
using MonolithTemplate.Identity.Application.Abstractions.Auth;
using Microsoft.AspNetCore.Identity;
using MonolithTemplate.Identity.Infrastructure.Database;

namespace MonolithTemplate.Identity.Infrastructure;

public static class Module
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = new[] { IdentityApplicationAssembly.GetAssembly,
            IdentityInfrastructureAssembly.GetAssembly
        };
        services.AddModuleShared(assemblies);

        var conn = configuration.GetConnectionString("default") ?? throw new ApplicationException("Connection string not found");

        services.AddAppDbContext<MyIdentityDbContext>(opt => opt.UseNpgsql(conn));
        services.AddIdentityDbTools();

        services.AddIdentityCore();

        return services;
    }

    public static IServiceCollection AddIdentityCore(this IServiceCollection services)
    {
        services.AddIdentityCore<User>(opt =>
        {
            opt.SignIn.RequireConfirmedEmail = false;
            opt.Password.RequireNonAlphanumeric = false;
            opt.User.RequireUniqueEmail = true;
        })
        .AddRoles<AppRole>()
        .AddEntityFrameworkStores<MyIdentityDbContext>()
        .AddSignInManager();

        services.AddScoped<ITokenGenerator, TokenGenerator>();

        services.AddAuthentication();

        return services;
    }

    private static IServiceCollection AddIdentityDbTools(this IServiceCollection services)
    {
        services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();

        services.AddScoped<IIdentityOutboxWriter, IdentityOutboxWriter>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(IdentityUnitOfWorkBehavior<,>));

        services.AddScoped<IOutboxModule>(sp =>
        {
            return new OutboxModule<MyIdentityDbContext>(
                sp.GetRequiredService<MyIdentityDbContext>(),
                sp.GetRequiredService<IEventBus>()
            );
        });

        return services;
    }
}
