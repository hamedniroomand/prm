using PRM.Application.Models;
using PRM.Contracts.Responses;

namespace PRM.API.Mapping;

public static class ProjectMapping
{
    public static ProjectResponse MapToProjectResponse(this Project project)
    {
        return new ProjectResponse()
        {
            Id = project.Id,
            Title = project.Title,
            ParentId = project.ParentId
        };
    }

    public static List<ProjectResponse> MapToProjectsResponse(this List<Project> projects)
    {
        List<ProjectResponse> result = [];
        foreach (var project in projects)
        {
            result.Add(project.MapToProjectResponse());
        }
        return result;
    }
}