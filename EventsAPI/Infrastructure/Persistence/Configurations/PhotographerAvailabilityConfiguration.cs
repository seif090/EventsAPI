using EventsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsAPI.Infrastructure.Persistence.Configurations;

public class PhotographerAvailabilityConfiguration : BaseEntityConfiguration<PhotographerAvailability>
{
    public override void Configure(EntityTypeBuilder<PhotographerAvailability> builder)
    {
        base.Configure(builder);

        builder.Property(slot => slot.Date).HasColumnType("date");
        builder.Property(slot => slot.StartTime).HasColumnType("time");
        builder.Property(slot => slot.EndTime).HasColumnType("time");

        builder.HasOne(slot => slot.Photographer)
            .WithMany(photographer => photographer.AvailabilitySlots)
            .HasForeignKey(slot => slot.PhotographerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(slot => new { slot.PhotographerId, slot.Date, slot.StartTime, slot.EndTime }).IsUnique();
    }
}
