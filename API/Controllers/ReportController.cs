using API.Models.Mappers;
using API.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly Repository _repository;
    private readonly ILogger<UserController> _logger;

    public ReportController(ILogger<UserController> logger, Repository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [Authorize]
    [HttpGet("GetUserMonthlyReport")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMonthlyReport([FromQuery] string userId, [FromQuery] int month)
    {
        // Validate the month parameter
        if (month < 1 || month > 12) return BadRequest("Invalid month. Please provide a value between 1 and 12.");

        // Query the repository
        var timeOffs = await _repository.TimeOffs
            .Where(b => b.UserId == userId && b.StartDate.Month == month)
            .ToListAsync();
        if (timeOffs.Count == 0) return NotFound($"The user with id: {userId} had no time offs in month {month}.");

        // Map to DTOs if necessary and return the result
        var output = timeOffs.Select(TimeOffMapper.CastModelToDto).ToList();

        return Ok(output);
    }

    [Authorize]
    [HttpGet("GetDailyUserReport")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetDailyUserReport([FromQuery] string userId, [FromQuery] int month)
    {
        // Validate the month parameter
        if (month < 1 || month > 12) return BadRequest("Invalid month. Please provide a value between 1 and 12.");

        // Query the repository
        var timeOffs = await _repository.TimeOffs
            .Where(b => b.UserId == userId && b.StartDate.Month == month)
            .ToListAsync();
        if (timeOffs.Count == 0) return NotFound($"The user with id: {userId} had no time offs in month {month}.");

        // Map to DTOs if necessary and return the result
        var output = timeOffs.Select(TimeOffMapper.CastModelToDto).ToList();

        return Ok(output);
    }
}