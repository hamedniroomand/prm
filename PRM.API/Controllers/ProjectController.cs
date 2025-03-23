using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRM.API.Mapping;
using PRM.Application.Services;
using PRM.Contracts.Exceptions;
using PRM.Contracts.Requests;

namespace PRM.API.Controllers;

[ApiController]
[Authorize]
public class ProjectController(ProjectService projectService) : ControllerBase
{
    [HttpGet(ApiEndpoints.Project.All)]
    public async Task<IActionResult> GetAll()
    {
        var projects = await projectService.GetAll();
        var response = projects.MapToProjectsResponse();
        return Ok(response);
    }

    [HttpPost(ApiEndpoints.Project.Create)]
    public async Task<IActionResult> Create(CreateProjectRequest request)
    {
        try
        {
            var project = await projectService.Create(request);
            var response = project.MapToProjectResponse();
            return CreatedAtAction(nameof(Get), new { id = project.Id }, response);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet(ApiEndpoints.Project.Get)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            var project = await projectService.Get(id);
            var response = project.MapToProjectResponse();
            return Ok(response);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete(ApiEndpoints.Project.Delete)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await projectService.Delete(id);
            return Accepted();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPut(ApiEndpoints.Project.Update)]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateProjectRequest request)
    {
        try
        {
            var project = await projectService.Update(id, request);
            var response = project.MapToProjectResponse();
            return Ok(response);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch
        {
            return BadRequest();
        }
    }
}