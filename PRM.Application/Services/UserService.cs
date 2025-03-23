using Microsoft.EntityFrameworkCore;
using PRM.Application.Models;
using PRM.Application.Repositories.User;

namespace PRM.Application.Services;

public class UserService(IUserRepository userRepository)
{
    public Task<User?> FindByUsername(string username)
    {
        return userRepository.GetQuery().FirstOrDefaultAsync(user => user.Username.ToLower() == username.ToLower());
    }
}