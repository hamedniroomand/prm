using Microsoft.AspNetCore.Mvc;
using PMS.API.Mapping;
using PMS.Application.Models;
using PMS.Application.Repositories;
using PMS.Contracts.Requests;

namespace PMS.API.Controllers;

[ApiController]
public class ProjectController(IProjectRepository projectRepository) : ControllerBase
{
    [HttpGet(ApiEndpoints.Project.All)]
    public async Task<IActionResult> GetAll()
    {
        var projects = await projectRepository.GetAllAsync();
        var response = projects.MapToProjectsResponse();
        return Ok(response);
    }

    [HttpPost(ApiEndpoints.Project.Create)]
    public async Task<IActionResult> Create(CreateProjectRequest body)
    {
        var project = await projectRepository.CreateAsync(new Project()
        {
            Title = body.Title,
            ParentId = body.ParentId
        });
        var response = project.MapToProjectResponse();
        return CreatedAtAction(nameof(Get), new { id = project.Id }, response);
    }

    [HttpGet(ApiEndpoints.Project.Get)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var project = await projectRepository.GetByIdAsync(id);
        if (project is null)
        {
            return NotFound();
        }

        var response = project.MapToProjectResponse();
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Project.Delete)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var project = await projectRepository.GetByIdAsync(id);
        if (project is null)
        {
            return NotFound();
        }

        await projectRepository.DeleteAsync(project);
        return Accepted();
    }

    [HttpPut(ApiEndpoints.Project.Update)]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateProjectRequest body)
    {
        var project = await projectRepository.GetByIdAsync(id);
        if (project is null)
        {
            return NotFound();
        }

        if (!String.IsNullOrEmpty(body.Title))
        {
            project.Title = body.Title;
        }

        if (body.ParentId is not null)
        {
            project.ParentId = body.ParentId;
        }

        await projectRepository.UpdateAsync(project);
        var response = project.MapToProjectResponse();
        return Ok(response);
    }
}