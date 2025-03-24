namespace PRM.Application.Models;

public enum UserRole
{
    SuperAdmin = 1,
    Admin = 2,
    Employee = 3,
}

public class User : EntityBase
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool Verified { get; set; }
    public Guid? VerificationCode { get; set; }
    public UserRole Role { get; set; } = UserRole.Employee;
    public DateTime? LastVisitedAt { get; set; }
    public IEnumerable<Task> Tasks { get; set; } = [];
    public List<Project> Projects { get; } = [];
}