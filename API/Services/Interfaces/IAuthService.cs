using API.Models.Entities;

namespace API.Services.Interfaces;

public interface IAuthService
{
    string GenerateJwtToken(AppUser appUser);

    Task<bool> VerifyUserCredentials(string username, string password);
}