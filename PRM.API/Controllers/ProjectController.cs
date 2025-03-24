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
}