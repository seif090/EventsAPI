using EventsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsAPI.Infrastructure.Persistence.Configurations;

public class AlbumTypeConfiguration : BaseEntityConfiguration<AlbumType>
{
    public override void Configure(EntityTypeBuilder<AlbumType> builder)
    {
        base.Configure(builder);

        builder.Property(album => album.Name).HasMaxLength(200).IsRequired();
        builder.Property(album => album.Size).HasMaxLength(100).IsRequired();
        builder.Property(album => album.BasePrice).HasPrecision(18, 2);

        builder.HasIndex(album => album.Name);
    }
}
