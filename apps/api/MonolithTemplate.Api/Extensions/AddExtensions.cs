
using MonolithTemplate.Api.Extensions.Exceptions;
using MonolithTemplate.Api.Extensions.Migrations;

namespace MonolithTemplate.Api.Extensions;

public static class AddExtensions
{
    public static IServiceCollection AddGlobalExceptions(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    public static async Task RunMigrations(this IHost app)
    {
        await app.MigrateAllDbContextsAsync();
    }

}
