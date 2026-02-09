using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using MonolithTemplate.Identity.Domain.IdentityModels;

namespace MonolithTemplate.Identity.Infrastructure.Database;

public sealed class IdentityDbContext : IdentityDbContext<User, AppRole, Guid>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("Identity");
    }

}
