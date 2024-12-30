using Microsoft.AspNetCore.Identity;

namespace API.Models.Entities;

public class UserEntity : IdentityUser
{
    public string RefreshToken { get; set; } = string.Empty;

    public DateTime RefreshTokenExpiry { get; set; }

    public ICollection<TimeOffEntity> TimeOffs { get; set; } = [];

    public ICollection<ShiftEntity> Shifts { get; set; } = [];
}