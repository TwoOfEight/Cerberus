namespace API.Models.DTOs;

public class TimeOffDto
{
    public required string Id { get; set; }
    public required string UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public required string Description { get; set; }
    public required string Status { get; set; }
}