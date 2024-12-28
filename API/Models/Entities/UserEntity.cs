using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API.Models.Entities;

public class UserEntity : IdentityUser
{
    public string RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpiry { get; set; }
    
    public List<TimeOffEntity> TimeOffs { get; set; } = [];
    
    public List<ShiftEntity> Shifts { get; set; } = [];
}