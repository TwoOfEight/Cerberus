using API.DTOs;
using API.Models.Authentication;
using API.Models.Entities;

namespace API.Models.Mappers;

public class UserMapper
{
    public static UserEntity DtoToUser(User input)
    {
        return new UserEntity()
        {
            // Id = input.Id,
            // UserName = input.UserName ?? string.Empty,
            // Name = input.Name ?? string.Empty,
            // Email = input.Email ?? string.Empty
        };
    }

    public static User EntityToDTO(UserEntity input)
    {
        return new User()
        {
            // Id = input.Id,
            // UserName = input.UserName ?? string.Empty,
            // Name = input.Name ?? string.Empty,
            // Email = input.Email ?? string.Empty
        };
    }

    public static UserEntity RegisterReqToEntity(RegistrationRequest input)
    {
        return new UserEntity()
        {
            // Email = input.Email,
            // Name = input.Name,
            // UserName = input.UserName
        };
    }
}