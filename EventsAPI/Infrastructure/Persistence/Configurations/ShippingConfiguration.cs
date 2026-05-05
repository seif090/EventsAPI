using EventsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsAPI.Infrastructure.Persistence.Configurations;

public class ShippingConfiguration : BaseEntityConfiguration<Shipping>
{
    public override void Configure(EntityTypeBuilder<Shipping> builder)
    {
        base.Configure(builder);

        builder.Property(shipping => shipping.ProviderName).HasMaxLength(200).IsRequired();
        builder.Property(shipping => shipping.TrackingNumber).HasMaxLength(200);
        builder.Property(shipping => shipping.Cost).HasPrecision(18, 2);
        builder.Property(shipping => shipping.AddressLine1).HasMaxLength(300).IsRequired();
        builder.Property(shipping => shipping.AddressLine2).HasMaxLength(300);
        builder.Property(shipping => shipping.City).HasMaxLength(100).IsRequired();
        builder.Property(shipping => shipping.Governorate).HasMaxLength(100).IsRequired();
        builder.Property(shipping => shipping.PostalCode).HasMaxLength(20).IsRequired();

        builder.HasOne(shipping => shipping.Order)
            .WithOne(order => order.Shipping)
            .HasForeignKey<Shipping>(shipping => shipping.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
