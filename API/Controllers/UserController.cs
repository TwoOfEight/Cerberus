using API.Models.DTOs;
using API.Models.Entities;
using API.Models.Mappers;
using API.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly Repository _repository;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, Repository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [Authorize]
    [HttpPost("AddTimeOff")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddTimeOff([FromBody] TimeOffCreateDto? requestBody, [FromQuery] string? userId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("User ID is required and cannot be null, empty, or whitespace.");
            if (requestBody == null) return BadRequest("TimeOff entity cannot be null.");

            var user = await _repository.AppUsers.FindAsync(userId);
            if (user == null) return NotFound("User not found.");

            var timeOff = TimeOffMapper.CastDtoToEntity(requestBody);
            user.TimeOffs.Add(timeOff);
            await _repository.SaveChangesAsync();

            return Ok(timeOff);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding time off.");
        }
    }

    [Authorize]
    [HttpGet("GetTimeOff")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTimeOff([FromQuery] string userId)
    {
        var output = await _repository.TimeOffs
            .Include(t => t.User)
            .Where(t => t.User != null && t.User.Id == userId)
            .ToListAsync();

        if (output.Count == 0) return NotFound($"The user with id: {userId} had no time offs.");

        return Ok(output.Select(TimeOffMapper.CastEntityToDto).ToList());
    }

    [Authorize]
    [HttpPut("UpdateTimeOff")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateTimeOff([FromBody] TimeOff request)
    {
        var timeOff = await _repository.TimeOffs.FindAsync(request.Id);

        if (timeOff == null) return NotFound("The time off could not be found.");

        timeOff.StartDate = request.StartDate;
        timeOff.EndDate = request.EndDate;
        timeOff.Duration = timeOff.EndDate.Subtract(timeOff.StartDate);
        timeOff.Reason = request.Reason;
        timeOff.Status = request.Status;

        await _repository.SaveChangesAsync();

        return Ok(timeOff);
    }

    [Authorize]
    [HttpGet("{userId}/TimeOffs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUserTimeOffs(string userId)
    {
        var user = await _repository.AppUsers
            .Include(u => u.TimeOffs)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null) return NotFound("User not found.");

        return Ok(user.TimeOffs);
    }

    [Authorize]
    [HttpDelete("DeleteTimeOff/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteTimeOff(string id)
    {
        try
        {
            var timeOff = await _repository.TimeOffs.FindAsync(id);

            if (timeOff == null) return NotFound("Time off not found.");

            _repository.TimeOffs.Remove(timeOff);
            await _repository.SaveChangesAsync();

            return Ok("Time off deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting time off.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting time off.");
        }
    }
}