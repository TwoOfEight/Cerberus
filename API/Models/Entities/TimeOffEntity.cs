using System.ComponentModel.DataAnnotations;

namespace API.Models.Entities;

public class TimeOffEntity
{
    public Guid Id { get; set; }
    
    [StringLength(256)]
    public required Guid UserId { get; set; }
    
    public UserEntity User { get; set; }  // Navigation property 
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public TimeSpan Duration => EndDate.Subtract(StartDate);

    public string? Reason { get; set; } = string.Empty;
    
    public string? Status { get; set; } = string.Empty;
}