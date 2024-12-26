using API.DTOs;
using API.Models.Authentication;
using API.Models.Entities;

namespace API.Services.Interfaces;

public interface IUserService
{
    Task<UserEntity> Create(RegistrationRequest request);

    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

    Task<bool> Delete(int id);

    Task<bool> Exists(int id);

    Task<ICollection<User>> GetAll();

    Task<User> GetById(int id);

    Task<UserEntity> GetByUsername(string username);

    Task<User> Update(User user);

    bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
}