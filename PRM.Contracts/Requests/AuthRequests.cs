using System.ComponentModel.DataAnnotations;

namespace PRM.Contracts.Requests;

public class LoginRequest
{
    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public required string Username { get; set; }

    [Required]
    [MinLength(8)]
    [MaxLength(100)]
    public required string Password { get; set; }
}