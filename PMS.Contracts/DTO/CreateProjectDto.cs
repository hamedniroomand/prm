namespace PMS.Contracts.DTO;

public class CreateProjectDto
{
    public required string Title { get; set; }
    public required int? PatentId { get; set; }
}