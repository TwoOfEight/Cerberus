namespace API.Models.Entities;

public class ShiftEntity
{
    public string Id { get; set; } = string.Empty;
    public required string UserId { get; set; }
    public required UserEntity User { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration => EndTime - StartTime;
    public string TaskDescription { get; set; } = string.Empty;
}