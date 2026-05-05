using EventsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsAPI.Infrastructure.Persistence.Configurations;

public class PhotoConfiguration : BaseEntityConfiguration<Photo>
{
    public override void Configure(EntityTypeBuilder<Photo> builder)
    {
        base.Configure(builder);

        builder.Property(photo => photo.Url).HasMaxLength(500).IsRequired();

        builder.HasOne(photo => photo.Booking)
            .WithMany(booking => booking.Photos)
            .HasForeignKey(photo => photo.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(photo => photo.Photographer)
            .WithMany()
            .HasForeignKey(photo => photo.PhotographerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
