using EventsAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsAPI.Infrastructure.Persistence.Configurations;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(user => user.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(user => user.LastName).HasMaxLength(100).IsRequired();
        builder.Property(user => user.Email).HasMaxLength(256).IsRequired();
        builder.Property(user => user.PasswordHash).HasMaxLength(500).IsRequired();
        builder.Property(user => user.PhoneNumber).HasMaxLength(30);
        builder.Property(user => user.ProfileImageUrl).HasMaxLength(500);
        builder.Property(user => user.EmailConfirmationToken).HasMaxLength(200);
        builder.Property(user => user.PasswordResetToken).HasMaxLength(200);

        builder.HasIndex(user => user.Email).IsUnique();

        builder.HasOne(user => user.PhotographerProfile)
            .WithOne(photographer => photographer.User)
            .HasForeignKey<Photographer>(photographer => photographer.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
