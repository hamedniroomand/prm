using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PRM.Application.Database;
using PRM.Application.Repositories;
using PRM.Application.Repositories.User;
using PRM.Application.Services;

namespace PRM.Application;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<ProjectService>();
        services.AddScoped<UserService>();
        services.AddScoped<AuthService>();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });
        return services;
    }
}