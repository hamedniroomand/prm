using PRM.Application.Models;
using PRM.Contracts.Responses;

namespace PRM.API.Mapping;

public static class UserMapping
{
    public static UserResponses.User MapToUserResponse(this User user)
    {
        return new UserResponses.User()
        {
            Id = user.Id,
            Name = user.Name,
            Username = user.Username
        };
    }
}