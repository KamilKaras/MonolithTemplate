using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonolithTemplate.Identity.Domain.IdentityModels;
using MonolithTemplate.Shared.Events;
using MonolithTemplate.Shared.OutboxPattern;

namespace MonolithTemplate.Identity.Infrastructure.Database;

public sealed class MyIdentityDbContext : IdentityDbContext<User, AppRole, Guid>
{
    public MyIdentityDbContext(DbContextOptions<MyIdentityDbContext> options) : base(options) { }
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(IdentityApplicationAssembly.GetAssembly);

        builder.HasDefaultSchema("Identity");
    }

}
