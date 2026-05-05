using EventsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsAPI.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : BaseEntityConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.Property(order => order.TotalPrice).HasPrecision(18, 2);

        builder.HasOne(order => order.Booking)
            .WithOne(booking => booking.Order)
            .HasForeignKey<Order>(order => order.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(order => order.Client)
            .WithMany(user => user.Orders)
            .HasForeignKey(order => order.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(order => order.AlbumType)
            .WithMany(album => album.Orders)
            .HasForeignKey(order => order.AlbumTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(order => order.BoxType)
            .WithMany(box => box.Orders)
            .HasForeignKey(order => order.BoxTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
