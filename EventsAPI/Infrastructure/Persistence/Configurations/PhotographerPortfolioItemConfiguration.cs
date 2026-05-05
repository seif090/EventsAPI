using EventsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsAPI.Infrastructure.Persistence.Configurations;

public class PhotographerPortfolioItemConfiguration : BaseEntityConfiguration<PhotographerPortfolioItem>
{
    public override void Configure(EntityTypeBuilder<PhotographerPortfolioItem> builder)
    {
        base.Configure(builder);

        builder.Property(item => item.Title).HasMaxLength(200).IsRequired();
        builder.Property(item => item.ImageUrl).HasMaxLength(500).IsRequired();

        builder.HasOne(item => item.Photographer)
            .WithMany(photographer => photographer.PortfolioItems)
            .HasForeignKey(item => item.PhotographerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
