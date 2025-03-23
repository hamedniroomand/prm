namespace PMS.Application.Models;

public class Project : EntityBase
{
    public string Title { get; set; }
    public int? ParentId { get; set; }
    public IEnumerable<Task> Tasks { get; set; } = [];
}