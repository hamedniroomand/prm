using PRM.Application.Database;
using PRM.Application.Repositories.ProjectAssignee;

namespace PRM.Application.Repositories;

public class ProjectAssigneeRepository(ApplicationDbContext dbContext) : GenericRepository<Models.ProjectAssignee>(dbContext), IProjectAssigneeRepository
{

}