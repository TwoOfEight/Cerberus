using Microsoft.AspNetCore.Identity;

namespace API.Models.Entities;

public class UserEntity : IdentityUser
{
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiry { get; set; }
    public ICollection<TimeOffEntity> TimeOffs { get; set; } = new List<TimeOffEntity>();
    public ICollection<ShiftEntity> Shifts { get; set; } = new List<ShiftEntity>();
}