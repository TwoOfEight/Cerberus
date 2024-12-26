using Microsoft.AspNetCore.Identity;

namespace API.Models.Entities;

public class UserEntity : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiry { get; set; }
}