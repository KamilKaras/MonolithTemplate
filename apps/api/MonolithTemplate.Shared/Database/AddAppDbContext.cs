using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MonolithTemplate.Shared.Database;

public static class AddDbContext
{
    public static IServiceCollection AddAppDbContext<T>(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        where T : DbContext
    {
        services.AddDbContext<T>(options);
        return services;
    }
}
