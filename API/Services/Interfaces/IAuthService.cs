using API.Models.Entities;

namespace API.Services.Interfaces;

public interface IAuthService
{
    string GenerateJwtToken(UserModel userModel);

    Task<bool> VerifyUserCredentials(string username, string password);
}