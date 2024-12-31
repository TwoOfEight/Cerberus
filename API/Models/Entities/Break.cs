using System.ComponentModel.DataAnnotations;

namespace API.Models.Entities;

public class Break
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();

    public string? UserId { get; set; } = string.Empty;

    public AppUser? User { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public TimeSpan Duration { get; set; }

    [StringLength(2048)] public string Reason { get; set; } = string.Empty;

    [StringLength(2048)] public string Status { get; set; } = string.Empty;
}