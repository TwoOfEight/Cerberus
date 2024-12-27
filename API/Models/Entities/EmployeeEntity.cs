namespace API.Models.Entities;

public class EmployeeEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Rank { get; set; }
    public string? UserId { get; set; }
    public UserEntity? User { get; set; }
}