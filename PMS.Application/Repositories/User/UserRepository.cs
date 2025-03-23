using PMS.Application.Database;

namespace PMS.Application.Repositories.User;

public class UserRepository(ApplicationDbContext dbContext) : GenericRepository<Models.User>(dbContext), IUserRepository;