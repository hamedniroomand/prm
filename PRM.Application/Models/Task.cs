namespace PRM.Application.Models;

public class Task : EntityBase
{
    public DateOnly Date { get; set; }
    public int Duration { get; set; } = 0;
    public string? Description { get; set; }
    public Project Project { get; set; }
    public User User { get; set; }
    public int ProjectId { get; set; }
    public int UserId { get; set; }
}