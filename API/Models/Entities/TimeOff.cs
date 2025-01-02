using System.ComponentModel.DataAnnotations;

namespace API.Models.Entities;

public class TimeOff
{
    public string? Id { get; set; }
    public string? UserId { get; set; }
    public AppUser? User { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
}