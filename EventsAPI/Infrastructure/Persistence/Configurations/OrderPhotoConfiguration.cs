using EventsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsAPI.Infrastructure.Persistence.Configurations;

public class OrderPhotoConfiguration : IEntityTypeConfiguration<OrderPhoto>
{
    public void Configure(EntityTypeBuilder<OrderPhoto> builder)
    {
        builder.HasKey(orderPhoto => new { orderPhoto.OrderId, orderPhoto.PhotoId });

        builder.HasOne(orderPhoto => orderPhoto.Order)
            .WithMany(order => order.OrderPhotos)
            .HasForeignKey(orderPhoto => orderPhoto.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(orderPhoto => orderPhoto.Photo)
            .WithMany(photo => photo.OrderPhotos)
            .HasForeignKey(orderPhoto => orderPhoto.PhotoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
