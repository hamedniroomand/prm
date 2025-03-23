using PRM.Application.Database;
using PRM.Application.Models;

namespace PRM.Application.Repositories;

public class ProjectRepository(ApplicationDbContext dbContext) : GenericRepository<Project>(dbContext), IProjectRepository;