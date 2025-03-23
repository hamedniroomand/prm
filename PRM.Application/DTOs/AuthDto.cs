using PRM.Contracts.Responses;

namespace PRM.API.DTOs;

public class AuthDto
{
    public class UserWithToken
    {
        public string Token { get; set; }
        public UserResponses.User User { get; set; }
    }
}