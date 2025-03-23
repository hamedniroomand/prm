using PMS.Application.Models;
using PMS.Contracts.Responses;

namespace PMS.API.Mapping;

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