
using System.ComponentModel.DataAnnotations;

namespace API.Models.Entities;

public class ShiftEntity
{
    public string Id { get; set; }
    
    public required Guid UserId { get; set; }
    
    public required UserEntity User { get; set; }  
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public TimeSpan Duration => EndTime - StartTime; 
    
    [StringLength(256)]
    public string TaskDescription { get; set; } = string.Empty;
}