namespace API.Models.Entities;

public class Shift
{
    public string Id { get; set; } = string.Empty;
    public required string UserId { get; set; }
    public required AppUser AppUser { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration => EndTime - StartTime;
    public string TaskDescription { get; set; } = string.Empty;
}