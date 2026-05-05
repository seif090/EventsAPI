using EventsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsAPI.Infrastructure.Persistence.Configurations;

public class PhotographerConfiguration : BaseEntityConfiguration<Photographer>
{
    public override void Configure(EntityTypeBuilder<Photographer> builder)
    {
        base.Configure(builder);

        builder.Property(photographer => photographer.Bio).HasMaxLength(1000);
        builder.Property(photographer => photographer.Location).HasMaxLength(200);
        builder.Property(photographer => photographer.PricePerHour).HasPrecision(18, 2);

        builder.HasIndex(photographer => photographer.UserId).IsUnique();
    }
}
