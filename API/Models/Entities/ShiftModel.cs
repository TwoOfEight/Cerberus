namespace API.Models.Entities;

public class ShiftModel
{
    public string Id { get; set; } = string.Empty;
    public required string UserId { get; set; }
    public required UserModel UserModel { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration => EndTime - StartTime;
    public string TaskDescription { get; set; } = string.Empty;
}