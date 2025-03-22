using PMS.Application.Database;
using PMS.Application.Models;

namespace PMS.Application.Repositories;

public class ProjectGenericRepository : GenericRepository<Project> , IProjectRepository
{
    public ProjectGenericRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}