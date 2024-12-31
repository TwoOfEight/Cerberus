using API.DTOs;
using API.Models.Authentication;
using API.Models.Entities;

namespace API.Models.Mappers;

public class UserMapper
{
    public static UserModel DtoToUser(User input)
    {
        return new UserModel
        {
            // Id = input.Id,
            // UserName = input.UserName ?? string.Empty,
            // Name = input.Name ?? string.Empty,
            // Email = input.Email ?? string.Empty
        };
    }

    public static User EntityToDTO(UserModel input)
    {
        return new User
        {
            // Id = input.Id,
            // UserName = input.UserName ?? string.Empty,
            // Name = input.Name ?? string.Empty,
            // Email = input.Email ?? string.Empty
        };
    }

    public static UserModel RegisterReqToEntity(RegistrationRequest input)
    {
        return new UserModel
        {
            // Email = input.Email,
            // Name = input.Name,
            // UserName = input.UserName
        };
    }
}