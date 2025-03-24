using System.Text.Json.Serialization;

namespace PRM.Application.Models;

public class ProjectAssignee : EntityBase
{
    public int UserId { get; set; }
    public int ProjectId { get; set; }

    [JsonIgnore]
    public Project Project { get; set; }
    [JsonIgnore]
    public User User { get; set; }
}