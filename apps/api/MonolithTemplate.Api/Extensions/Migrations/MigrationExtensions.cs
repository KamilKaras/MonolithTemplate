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

            var dbContextTypes = services
                .GetServices<DbContextOptions>()
                .Select(o => o.ContextType)
                .Distinct()
                .ToList();

            var logger = services.GetRequiredService<ILoggerFactory>()
                     .CreateLogger("DbMigration");

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
