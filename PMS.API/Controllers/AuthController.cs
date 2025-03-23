using Microsoft.AspNetCore.Mvc;
using PMS.Application.Services;
using PMS.Contracts.Exceptions;
using PMS.Contracts.Requests;
using PMS.Contracts.Responses;

namespace PMS.API.Controllers;

[ApiController]
public class AuthController(AuthService authService) : Controller
{
    [HttpPost(ApiEndpoints.Auth.Login)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var token = await authService.Login(request.Username, request.Password);
            var response = new AuthResponses.Login()
            {
                Token = token,
            };
            return Accepted(response);
        }
        catch (NotFoundException)
        {
            return Unauthorized();
        }
        catch (UnauthorizedException)
        {
            return Unauthorized();
        }
        catch (Exception err)
        {
            return BadRequest(err);
        }
    }
}