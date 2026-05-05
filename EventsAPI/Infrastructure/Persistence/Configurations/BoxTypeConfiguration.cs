using EventsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsAPI.Infrastructure.Persistence.Configurations;

public class BoxTypeConfiguration : BaseEntityConfiguration<BoxType>
{
    public override void Configure(EntityTypeBuilder<BoxType> builder)
    {
        base.Configure(builder);

        builder.Property(box => box.Name).HasMaxLength(200).IsRequired();
        builder.Property(box => box.Material).HasMaxLength(100).IsRequired();
        builder.Property(box => box.BasePrice).HasPrecision(18, 2);

        builder.HasIndex(box => box.Name);
    }
}
