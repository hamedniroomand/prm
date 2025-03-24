using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRM.API.Mapping;
using PRM.Application.Services;
using PRM.Contracts.Exceptions;
using PRM.Contracts.Requests;

namespace PRM.API.Controllers;

[ApiController]
[Authorize]
public class ProjectController(ProjectService projectService, IHttpContextAccessor httpContextAccessor) : ControllerBase
{
    [HttpGet(ApiEndpoints.Project.All)]
    public async Task<IActionResult> GetAll()
    {
        var claims = httpContextAccessor.HttpContext?.User.Claims;
        var username = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
        var projects = await projectService.GetAllUserAssignees(username);
        var response = projects.MapToProjectsResponse();
        return Ok(response);
    }
}