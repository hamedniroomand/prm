using PRM.Application.Database;

namespace PRM.Application.Repositories.User;

public class UserRepository(ApplicationDbContext dbContext) : GenericRepository<Models.User>(dbContext), IUserRepository;