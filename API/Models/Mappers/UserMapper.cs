using API.DTOs;
using API.Models.Authentication;
using API.Models.Entities;

namespace API.Models.Mappers;

public class UserMapper
{
    public static AppUser DtoToUser(User input)
    {
        return new AppUser
        {
            // Id = input.Id,
            // UserName = input.UserName ?? string.Empty,
            // Name = input.Name ?? string.Empty,
            // Email = input.Email ?? string.Empty
        };
    }

    public static User EntityToDTO(AppUser input)
    {
        return new User
        {
            // Id = input.Id,
            // UserName = input.UserName ?? string.Empty,
            // Name = input.Name ?? string.Empty,
            // Email = input.Email ?? string.Empty
        };
    }

    public static AppUser RegisterReqToEntity(RegistrationRequest input)
    {
        return new AppUser
        {
            // Email = input.Email,
            // Name = input.Name,
            // UserName = input.UserName
        };
    }
}