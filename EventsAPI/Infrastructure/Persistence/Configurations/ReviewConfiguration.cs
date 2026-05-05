using EventsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsAPI.Infrastructure.Persistence.Configurations;

public class ReviewConfiguration : BaseEntityConfiguration<Review>
{
    public override void Configure(EntityTypeBuilder<Review> builder)
    {
        base.Configure(builder);

        builder.Property(review => review.Rating).IsRequired();
        builder.Property(review => review.Comment).HasMaxLength(1000);

        builder.HasOne(review => review.Booking)
            .WithMany()
            .HasForeignKey(review => review.BookingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(review => review.Photographer)
            .WithMany()
            .HasForeignKey(review => review.PhotographerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(review => review.Client)
            .WithMany(user => user.Reviews)
            .HasForeignKey(review => review.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(review => review.BookingId).IsUnique();
    }
}
