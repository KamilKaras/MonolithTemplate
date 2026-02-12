using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonolithTemplate.Shared.OutboxPattern;

namespace MonolithTemplate.Identity.Infrastructure.Database.Configurations;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> b)
    {
        b.ToTable("OutboxMessages");

        b.HasKey(x => x.Id);

        b.Property(x => x.Type)
            .IsRequired();

        b.Property(x => x.Payload)
            .IsRequired();

        b.Property(x => x.OccurredOnUtc)
            .IsRequired();

        b.HasIndex(x => x.ProcessedOnUtc);
    }
}