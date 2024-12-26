namespace API.Models.Authentication;

public class LoginResponse
{
    public DateTime ExpirationDate { get; set; }
    public required string JwtToken { get; set; }
    public required string RefreshToken { get; set; }
}