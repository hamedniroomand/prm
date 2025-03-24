namespace PRM.Application.Models;

public class Task : EntityBase
{
    public required DateOnly Date { get; set; }
    public required int Duration { get; set; } = 0;
    public required string? Description { get; set; }
    public required int ProjectId { get; set; }
    public required int UserId { get; set; }
}