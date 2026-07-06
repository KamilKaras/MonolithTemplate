using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace MonolithTemplate.Api.Extensions.Migrations;

public static class MigrationExtensions
{
    public static async Task MigrateAllDbContextsAsync(this IHost app)
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILoggerFactory>()
                .CreateLogger("DbMigration");

            var dbContextTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly =>
                {
                    try
                    {
                        return assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        return ex.Types.Where(type => type is not null)!;
                    }
                })
                .Where(type =>
                    type is not null &&
                    !type.IsAbstract &&
                    typeof(DbContext).IsAssignableFrom(type))
                .Cast<Type>()
                .Where(type => services.GetService(type) is DbContext)
                .Distinct()
                .ToList();

            foreach (var dbContextType in dbContextTypes)
            {
                logger.LogInformation("Migrating DbContext: {DbContext}", dbContextType.Name);
                var db = (DbContext)services.GetRequiredService(dbContextType);
                await db.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Problem during database migration", ex);
        }

    }
}
