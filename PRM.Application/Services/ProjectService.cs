using Microsoft.EntityFrameworkCore;
using PRM.Application.Models;
using PRM.Application.Repositories;
using PRM.Application.Repositories.ProjectAssignee;
using PRM.Contracts.Exceptions;
using PRM.Contracts.Requests;
using Task = System.Threading.Tasks.Task;

namespace PRM.Application.Services;

public class ProjectService(IProjectRepository projectRepository, IProjectAssigneeRepository projectAssigneeRepository, UserService userService)
{
    public Task<List<Project>> GetAll()
    {
        return projectRepository.GetAllAsync().ToListAsync();
    }

    public Task<Project> Create(CreateProjectRequest request)
    {
        return projectRepository.CreateAsync(new Project()
        {
            Title = request.Title,
            ParentId = request.ParentId
        });
    }

    public async Task<Project> Get(int id)
    {
        var project = await projectRepository.GetByIdAsync(id);
        if (project is null)
        {
            throw new NotFoundException();
        }

        return project;
    }

    public async Task<Project> Update(int id, UpdateProjectRequest request)
    {
        var project = await projectRepository.GetByIdAsync(id);
        if (project is null)
        {
            throw new NotFoundException();
        }

        if (!String.IsNullOrEmpty(request.Title))
        {
            project.Title = request.Title;
        }

        if (request.ParentId is not null)
        {
            project.ParentId = request.ParentId;
        }

        return await projectRepository.UpdateAsync(project);
    }

    public async Task Delete(int id)
    {
        var project = await Get(id);
        await projectRepository.DeleteAsync(project);
    }

    public async Task<ProjectAssignee> AssignToUser(int projectId, int userId)
    {
        var project = await projectRepository.GetByIdAsync(projectId);
        var user = await userService.GetByIdAsync(userId);
        if (user is null || project is null)
        {
            throw new NotFoundException();
        }

        return await projectAssigneeRepository.CreateAsync(new ProjectAssignee()
        {
            ProjectId = project.Id,
            UserId = user.Id,

        });
    }

    public async Task<List<Project>> GetAllUserAssignees(User user)
    {
        return await projectRepository.GetAllAsync()
            .Include(project => project.Users)
            .Where(project => project.Users.Any(u => u.Id == user.Id))
            .ToListAsync();
    }
}