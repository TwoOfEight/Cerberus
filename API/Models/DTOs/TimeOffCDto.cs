namespace API.Models.DTOs;

public class TimeOffCDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public required string Description { get; set; }
    public required string Status { get; set; }
}