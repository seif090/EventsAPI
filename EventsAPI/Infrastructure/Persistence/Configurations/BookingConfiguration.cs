using EventsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsAPI.Infrastructure.Persistence.Configurations;

public class BookingConfiguration : BaseEntityConfiguration<Booking>
{
    public override void Configure(EntityTypeBuilder<Booking> builder)
    {
        base.Configure(builder);

        builder.Property(booking => booking.ScheduledAt).IsRequired();
        builder.Property(booking => booking.DurationMinutes).IsRequired();
        builder.Property(booking => booking.Price).HasPrecision(18, 2);
        builder.Property(booking => booking.Notes).HasMaxLength(1000);

        builder.HasOne(booking => booking.Client)
            .WithMany(user => user.BookingsAsClient)
            .HasForeignKey(booking => booking.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(booking => booking.Photographer)
            .WithMany(photographer => photographer.Bookings)
            .HasForeignKey(booking => booking.PhotographerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(booking => new { booking.PhotographerId, booking.ScheduledAt });
    }
}
