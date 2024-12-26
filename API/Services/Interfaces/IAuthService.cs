using API.Models.Entities;

namespace API.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(UserEntity user);

        Task<bool> VerifyUserCredentials(string username, string password);
    }
}