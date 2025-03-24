using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PRM.Application.Database.Seeders;
using PRM.Application.Models;
using Task = PRM.Application.Models.Task;

namespace PRM.Application.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<ProjectAssignee> ProjectAssignees { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        Projects = Set<Project>();
        Users = Set<User>();
        Projects = Set<Project>();
        ProjectAssignees = Set<ProjectAssignee>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SeedUserData();

        modelBuilder.Entity<User>()
            .HasIndex(x => x.Username)
            .IsUnique();

        modelBuilder.Entity<Task>()
            .HasOne<Project>()
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade); // Tasks will auto-delete when Project is deleted

        modelBuilder.Entity<Task>()
            .HasOne<User>()
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent accidental task deletion via User

        modelBuilder.Entity<User>()
            .HasMany(user => user.Projects)
            .WithMany(project => project.Users)
            .UsingEntity<ProjectAssignee>();

        modelBuilder.Entity<ProjectAssignee>()
            .HasIndex(x => new { x.ProjectId, x.UserId })
            .IsUnique();
    }
}
