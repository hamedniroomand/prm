using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PRM.Application.Models;
using PRM.Application.Repositories.User;
using PRM.Contracts.Exceptions;
using PRM.Contracts.Requests.admin;

namespace PRM.Application.Services;

public class UserService(IUserRepository userRepository)
{
    public Task<User?> FindByUsername(string username)
    {
        return userRepository.GetQuery().FirstOrDefaultAsync(user => user.Username.ToLower() == username.ToLower());
    }

    public Task<List<User>> GetAll()
    {
        return userRepository.GetAllAsync().ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        return user;
    }

    public async Task<User> CreateUser(AdminUserRequests.Create request)
    {
        var existingUser = await FindByUsername(request.Username);
        if (existingUser != null)
        {
            throw new Exception("Username already exists");
        }
        var passwordHasher = new PasswordHasher<User>();
        var user = new User()
        {
            Username = request.Username,
            Name = request.Name,
            Password = passwordHasher.HashPassword(null, Guid.NewGuid().ToString()),
            Role = request.IsAdmin ? UserRole.Admin : UserRole.Employee,
            VerificationCode = Guid.NewGuid(),
            Verified = false,
        };
        return await userRepository.CreateAsync(user);
    }
}