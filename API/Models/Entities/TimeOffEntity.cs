using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Entities;

public class TimeOffEntity
{
    public string Id { get; set; } = string.Empty;

    public required string UserId { get; set; }
    
    public required UserEntity User { get; set; } 
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public TimeSpan Duration => EndDate.Subtract(StartDate);

    public string Reason { get; set; } = string.Empty;
    
    public string Status { get; set; } = string.Empty;
}