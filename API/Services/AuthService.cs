//using API.Services.Interfaces;

//using Data.Models;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace API.Services;

//public class AuthService : IAuthService
//{
//    private readonly IConfiguration _configuration;
//    private readonly IUserService _userService;

//    public AuthService(IUserService userService, IConfiguration configuration)
//    {
//        _userService = userService;
//        _configuration = configuration;
//    }

//    public string GenerateJwtToken(User user)
//    {
//        string? jwtKey = _configuration["Jwt:Key"];
//        string? jwtIssuer = _configuration["Jwt:Issuer"];
//        string? jwtAudience = _configuration["Jwt:Audience"];

//        if (string.IsNullOrEmpty(jwtKey) ||
//            string.IsNullOrEmpty(jwtIssuer) ||
//            string.IsNullOrEmpty(jwtAudience))
//        {
//            throw new InvalidOperationException("JWT configuration is invalid. Ensure 'Key', 'Issuer', and 'Audience' are set.");
//        }

//        var key = Encoding.UTF8.GetBytes(jwtKey);
//        var tokenHandler = new JwtSecurityTokenHandler();

//        var tokenDescriptor = new SecurityTokenDescriptor
//        {
//            Subject = new ClaimsIdentity(new[]
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim(ClaimTypes.Name, user.UserName),
//                new Claim(ClaimTypes.Role, user.Role)
//            }),
//            Expires = DateTime.UtcNow.AddHours(1),
//            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
//            Issuer = jwtIssuer,
//            Audience = jwtAudience
//        };

//        var token = tokenHandler.CreateToken(tokenDescriptor);
//        return tokenHandler.WriteToken(token);
//    }

//    public async Task<bool> VerifyUserCredentials(string username, string password)
//    {
//        var user = await _userService.GetByUsername(username);
//        if (user == null) return false;

//        return _userService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
//    }
//}