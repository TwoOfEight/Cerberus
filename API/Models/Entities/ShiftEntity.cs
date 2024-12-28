using System.ComponentModel.DataAnnotations;

namespace API.Models.Entities;

public class ShiftEntity
{
    public Guid Id { get; set; }

    [StringLength(256)]
    public required Guid UserId { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public TimeSpan Duration => EndTime - StartTime; 
    
    public string TaskDescription { get; set; } = string.Empty;
}