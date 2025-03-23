using Microsoft.EntityFrameworkCore;
using PMS.Application.Models;
using PMS.Application.Repositories.User;

namespace PMS.Application.Services;

public class UserService(IUserRepository userRepository)
{
    public Task<User?> FindByUsername(string username)
    {
        return userRepository.GetQuery().FirstOrDefaultAsync(user => user.Username.ToLower() == username.ToLower());
    }
}