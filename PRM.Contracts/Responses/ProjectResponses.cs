namespace PMS.Contracts.Responses;

public class ProjectResponse
{
    public required int Id { get; init; }
    public required string Title { get; init; }
    public int? ParentId { get; init; }
}