using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PRM.Application.Common;
using PRM.Application.Models;
using PRM.Contracts.Exceptions;

namespace PRM.Application.Services;

public class AuthService(UserService userService, ApplicationSettings applicationSettings)
{
    public async Task<string> Login(string username, string password)
    {
        try
        {
            var user = await userService.FindByUsername(username);
            if (user is null)
            {
                throw new NotFoundException();
            }

            var hasher = new PasswordHasher<User>();
            var verificationResult = hasher.VerifyHashedPassword(null, user.Password, password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedException();
            }

            var token = GenerateJwtToken(user);

            return token;
        }
        catch (Exception ex) when (ex is not NotFoundException && ex is not UnauthorizedException)
        {
            throw new Exception("An error occurred during authentication", ex);
        }
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(applicationSettings.JwtString));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}