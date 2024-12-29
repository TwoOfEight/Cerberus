using System.ComponentModel.DataAnnotations;

namespace API.Models.Entities;

public class TimeOffEntity
{
    public string Id { get; set; }
    
    public required string UserId { get; set; }
    
    public required UserEntity User { get; set; } 
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public TimeSpan Duration => EndDate.Subtract(StartDate);
    
    [StringLength(256)]
    public string? Reason { get; set; } = string.Empty;
    
    [StringLength(256)]
    public string? Status { get; set; } = string.Empty;
}