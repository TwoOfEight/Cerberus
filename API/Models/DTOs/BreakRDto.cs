namespace API.Models.DTOs;

public class BreakRDto
{
    public required string Id { get; set; }
    public required string UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan Duration { get; set; }
    public required string Reason { get; set; }
    public required string Status { get; set; }
}