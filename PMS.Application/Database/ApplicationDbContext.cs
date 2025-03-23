using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Database.Seeders;
using PMS.Application.Models;
using Task = PMS.Application.Models.Task;

namespace PMS.Application.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        Projects = Set<Project>();
        Users = Set<User>();
        Projects = Set<Project>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SeedUserData();

        modelBuilder.Entity<Task>()
            .HasOne(x => x.Project)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade); // Tasks will auto-delete when Project is deleted

        modelBuilder.Entity<Task>()
            .HasOne<User>()
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent accidental task deletion via User
    }
}
