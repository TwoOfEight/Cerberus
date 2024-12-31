using Microsoft.AspNetCore.Identity;

namespace API.Models.Entities;

public class UserModel : IdentityUser
{
    public string RefreshToken { get; set; } = string.Empty;

    public DateTime RefreshTokenExpiry { get; set; }

    public ICollection<TimeOffModel> TimeOffs { get; set; } = [];

    public ICollection<ShiftModel> Shifts { get; set; } = [];
}