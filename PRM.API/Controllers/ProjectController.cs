using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRM.API.Attributes;
using PRM.API.Mapping;
using PRM.Application.Models;
using PRM.Application.Services;

namespace PRM.API.Controllers;

[ApiController]
[Authorize]
public class ProjectController(ProjectService projectService, IHttpContextAccessor httpContextAccessor) : ControllerBase
{
    [HttpGet(ApiEndpoints.Project.All)]
    public async Task<IActionResult> GetAll([CurrentUser] User user)
    {
        var projects = await projectService.GetAllUserAssignees(user);
        var response = projects.MapToProjectsResponse();
        return Ok(response);
    }
}