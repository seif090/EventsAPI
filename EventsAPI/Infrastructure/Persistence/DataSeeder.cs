using EventsAPI.Domain.Entities;
using EventsAPI.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventsAPI.Infrastructure.Persistence;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
    {
        await dbContext.Database.MigrateAsync();

        if (!await dbContext.Users.AnyAsync())
        {
            var hasher = new PasswordHasher<User>();
            var admin = new User
            {
                FirstName = "System",
                LastName = "Admin",
                Email = "admin@events.local",
                Role = UserRole.Admin,
                IsEmailConfirmed = true
            };
            admin.PasswordHash = hasher.HashPassword(admin, "Admin@123");
            dbContext.Users.Add(admin);
        }

        if (!await dbContext.AlbumTypes.AnyAsync())
        {
            dbContext.AlbumTypes.AddRange(
                new AlbumType { Name = "Classic", Size = "30x30", PageCount = 20, BasePrice = 1200m },
                new AlbumType { Name = "Premium", Size = "40x30", PageCount = 30, BasePrice = 2000m });
        }

        if (!await dbContext.BoxTypes.AnyAsync())
        {
            dbContext.BoxTypes.AddRange(
                new BoxType { Name = "Standard Box", Material = "Wood", BasePrice = 350m },
                new BoxType { Name = "Luxury Box", Material = "Leather", BasePrice = 600m });
        }

        await dbContext.SaveChangesAsync();
    }
}
