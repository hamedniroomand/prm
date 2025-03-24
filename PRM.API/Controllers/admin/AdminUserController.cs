using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRM.API.Common;
using PRM.API.Mapping;
using PRM.Application.Services;
using PRM.Contracts.Exceptions;
using PRM.Contracts.Requests.admin;

namespace PRM.API.Controllers.admin;

[ApiController]
[Authorize(Roles = AuthConstants.AdminOrSuperAdminRoles)]
public class AdminUserController(UserService userService) : Controller
{
    [HttpGet(ApiEndpoints.Admin.User.All)]
    public async Task<IActionResult> GetALl()
    {
        var users = await userService.GetAll();
        var response = users.MapToUsersResponse();
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Admin.User.Get)]
    public async Task<IActionResult> GetById(int id)
    {
        try {
            var user = await userService.GetByIdAsync(id);
            var response = user.MapToUserResponse();
            return Ok(response);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost(ApiEndpoints.Admin.User.Create)]
    public async Task<IActionResult> Create(AdminUserRequests.Create request)
    {
        try
        {
            var user = await userService.CreateUser(request);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user.MapToUserResponse());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}