using PMS.Application.Database;
using PMS.Application.Models;

namespace PMS.Application.Repositories;

public class ProjectRepository(ApplicationDbContext dbContext) : GenericRepository<Project>(dbContext), IProjectRepository;