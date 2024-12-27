namespace API.Models.Entities;

public class WorkEntity
{
    public Guid Id { get; set; }

    public required Guid UserId { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public TimeSpan Duration => EndTime - StartTime; 
    
    public string TaskDescription { get; set; } = string.Empty;
}