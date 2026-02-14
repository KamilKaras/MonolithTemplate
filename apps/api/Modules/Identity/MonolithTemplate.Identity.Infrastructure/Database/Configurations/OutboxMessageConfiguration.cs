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

        b.Property(x => x.AttemptCount)
            .IsRequired();

        b.Property(x => x.Error);

        b.Property(x => x.ProcessedOnUtc);

        b.HasIndex(x => x.ProcessedOnUtc);
    }
}