using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Models;

namespace PMS.Application.Database.Seeders;

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
                Password = hasher.HashPassword(null, Guid.NewGuid().ToString())
            }
        );
        return builder;
    }
}