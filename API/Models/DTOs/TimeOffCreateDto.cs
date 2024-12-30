namespace API.Models.DTOs;

public class TimeOffCreateDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public required string Reason { get; set; }
    public required string Status { get; set; }
}