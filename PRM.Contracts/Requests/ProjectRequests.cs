using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PRM.Contracts.Requests;

public class CreateProjectRequest
{
    [MinLength(3)]
    [MaxLength(100)]
    [Required]
    public required string Title { get; init; }

    [AllowNull()]
    public int? ParentId { get; set; } = null;
}

public class UpdateProjectRequest
{
    [MinLength(3)]
    [MaxLength(100)]
    public required string? Title { get; init; }
    public int? ParentId { get; init; }
}