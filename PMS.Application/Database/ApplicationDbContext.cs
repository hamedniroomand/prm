using Microsoft.EntityFrameworkCore;
using PMS.Application.Models;

namespace PMS.Application.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        Projects = Set<Project>();
    }
}
