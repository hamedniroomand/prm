using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PMS.Application.Common;
using PMS.Application.Models;
using PMS.Contracts.Exceptions;

namespace PMS.Application.Services;

public class AuthService(UserService userService, ApplicationSettings applicationSettings, ILogger<AuthService> logger)
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
            logger.LogError(ex, "Unexpected error during login");
            throw new Exception("An error occurred during authentication", ex);
        }
    }

    private string GenerateJwtToken(User user)
    {
        try
        {
            logger.LogInformation("1");
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Sid, user.Username),
                new Claim("Id", user.Id.ToString())
            };
            logger.LogInformation("2");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(applicationSettings.JwtString));
            logger.LogInformation("3");
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            logger.LogInformation("4");

            var token = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: credentials
            );
            logger.LogInformation("5");

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            logger.LogInformation($"Generated token: {tokenString.Substring(0, 20)}..."); // Log part of the token
            return tokenString;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error generating JWT token");
            throw;
        }
    }
}