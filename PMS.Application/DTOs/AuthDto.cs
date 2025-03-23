using PMS.Contracts.Responses;

namespace PMS.API.DTOs;

public class AuthDto
{
    public class UserWithToken
    {
        public string Token { get; set; }
        public UserResponses.User User { get; set; }
    }
}