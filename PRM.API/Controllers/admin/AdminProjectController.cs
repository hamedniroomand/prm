using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRM.API.Common;
using PRM.API.Mapping;
using PRM.Application.Services;
using PRM.Contracts.Exceptions;
using PRM.Contracts.Requests;
using PRM.Contracts.Requests.admin;

namespace PRM.API.Controllers.admin
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "Admin")]
    [Authorize(Roles = AuthConstants.AdminOrSuperAdminRoles)]
    public class AdminProjectController(ProjectService projectService) : Controller
    {

        [HttpPost(ApiEndpoints.Admin.Project.Create)]
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

        [HttpGet(ApiEndpoints.Admin.Project.All)]
        public async Task<IActionResult> GetAll()
        {
            var projects = await projectService.GetAll();
            var response = projects.MapToProjectsResponse();
            return Ok(response);
        }

        [HttpGet(ApiEndpoints.Admin.Project.Get)]
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

        [HttpPut(ApiEndpoints.Admin.Project.Update)]
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

        [Authorize(Roles = AuthConstants.SuperAdminUserPolicyName)]
        [HttpDelete(ApiEndpoints.Admin.Project.Delete)]
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

        [HttpPost(ApiEndpoints.Admin.Project.Assign)]
        public async Task<IActionResult> Assign([FromRoute] int id, [FromBody] AdminProjectRequests.Assign request)
        {
            try
            {
                var assignee = await projectService.AssignToUser(id, request.UserId);
                return Ok(assignee);
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
}