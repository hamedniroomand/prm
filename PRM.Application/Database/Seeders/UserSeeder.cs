using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PRM.Application.Models;

namespace PRM.Application.Database.Seeders;

public static class UserSeeder
{
    public static ModelBuilder SeedUserData(this ModelBuilder builder)
    {
        var hasher = new PasswordHasher<User>();

        builder.Entity<User>().HasData(
            new User()
            {
                Id = 1,
                Name = "ادمین",
                Role = UserRole.SuperAdmin,
                Verified = true,
                Username = "admin",
                Password = hasher.HashPassword(null, "Th@les123")
            }
        );
        return builder;
    }
}