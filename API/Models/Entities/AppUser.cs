using Microsoft.AspNetCore.Identity;

namespace API.Models.Entities;

public class AppUser : IdentityUser
{
    public string RefreshToken { get; set; } = string.Empty;

    public DateTime RefreshTokenExpiry { get; set; }

    public ICollection<TimeOff> TimeOffs { get; set; } = [];

    public ICollection<Shift> Shifts { get; set; } = [];
}