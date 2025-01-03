﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.Models.Authentication;
using API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthenticationController> _logger;
    private readonly UserManager<AppUser> _userManager;

    public AuthenticationController(UserManager<AppUser> userManager, IConfiguration configuration,
        ILogger<AuthenticationController> logger)
    {
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        _logger.LogInformation("Login called");

        var user = await _userManager.FindByNameAsync(model.Username);

        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password)) return Unauthorized();

        var token = GenerateJwt(model.Username);

        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddMinutes(60);

        await _userManager.UpdateAsync(user);

        _logger.LogInformation("Login succeeded");

        return Ok(new AuthenticationResponse
        {
            JwtToken = new JwtSecurityTokenHandler().WriteToken(token),
            ExpirationDate = token.ValidTo,
            RefreshToken = refreshToken,
            UserId = user.Id
        });
    }

    [HttpPost("Refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest model)
    {
        _logger.LogInformation("Refresh called");

        var principal = GetPrincipalFromExpiredToken(model.AccessToken);

        if (principal?.Identity?.Name is null) return Unauthorized();

        var user = await _userManager.FindByNameAsync(principal.Identity.Name);

        if (user is null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
            return Unauthorized();

        var token = GenerateJwt(principal.Identity.Name);

        _logger.LogInformation("Refresh succeeded");

        return Ok(new AuthenticationResponse
        {
            JwtToken = new JwtSecurityTokenHandler().WriteToken(token),
            ExpirationDate = token.ValidTo,
            RefreshToken = model.RefreshToken,
            UserId = user.Id
        });
    }

    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest model)
    {
        _logger.LogInformation("Register called");

        var existingUser = await _userManager.FindByNameAsync(model.Username);

        if (existingUser != null) return Conflict("User already exists.");

        var newUser = new AppUser
        {
            UserName = model.Username,
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(newUser, model.Password);

        if (result.Succeeded) return Ok("User successfully created");

        return StatusCode(
            StatusCodes.Status500InternalServerError,
            $"Failed to create user: {string.Join(" ", result.Errors.Select(e => e.Description))}"
        );
    }

    [Authorize]
    [HttpDelete("Revoke")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Revoke()
    {
        _logger.LogInformation("Revoke called");

        var username = HttpContext.User.Identity?.Name;

        if (username is null)
            return Unauthorized();

        var user = await _userManager.FindByNameAsync(username);

        if (user is null)
            return Unauthorized();

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);

        _logger.LogInformation("Revoke succeeded");

        return Ok();
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var generator = RandomNumberGenerator.Create();
        generator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private JwtSecurityToken GenerateJwt(string username)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ??
                                                                  throw new InvalidOperationException(
                                                                      "Secret not configured")));

        var token = new JwtSecurityToken(
            _configuration["JWT:ValidIssuer"],
            _configuration["JWT:ValidAudience"],
            expires: DateTime.UtcNow.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var secret = _configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured");

        var validation = new TokenValidationParameters
        {
            ValidIssuer = _configuration["JWT:ValidIssuer"] ??
                          throw new InvalidOperationException("ValidIssuer not configured"),
            ValidAudience = _configuration["JWT:ValidAudience"] ??
                            throw new InvalidOperationException("ValidAudience not configured"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ValidateLifetime = false
        };

        return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
    }
}