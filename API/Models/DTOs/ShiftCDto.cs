namespace API.Models.DTOs;

public class ShiftCDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public required string Description { get; set; }
}