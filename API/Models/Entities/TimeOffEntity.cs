using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models.Entities;

public class TimeOffEntity
{
    [Key] public string? Id { get; set; } = Guid.NewGuid().ToString();

    public string? UserId { get; set; } = string.Empty;

    [JsonIgnore] public UserEntity? User { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public TimeSpan Duration { get; set; }

    [StringLength(2048)] public string Reason { get; set; } = string.Empty;

    [StringLength(2048)] public string Status { get; set; } = string.Empty;
}