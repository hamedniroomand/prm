using PMS.Application.Models;
using PMS.Application.Repositories;
using PMS.Contracts.Exceptions;
using PMS.Contracts.Requests;
using Task = System.Threading.Tasks.Task;

namespace PMS.Application.Services;

public class ProjectService(IProjectRepository projectRepository)
{
    public Task<List<Project>> GetAll()
    {
        return projectRepository.GetAllAsync();
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
}