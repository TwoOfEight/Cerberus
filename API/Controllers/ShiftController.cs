using API.Models.DTOs;
using API.Models.Mappers;
using API.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ShiftController : ControllerBase
{
    private readonly Repository _repository;
    private readonly ILogger<UserController> _logger;

    public ShiftController(ILogger<UserController> logger, Repository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [Authorize]
    [HttpPost("AddShift")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddShift([FromBody] ShiftCDto? requestBody, [FromQuery] string? userId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest("User ID is required and cannot be null, empty, or whitespace.");
            if (requestBody == null) return BadRequest("Entity cannot be null.");

            var user = await _repository.AppUsers.FindAsync(userId);
            if (user is null) return NotFound("User not found.");

            var shift = ShiftMapper.CastCreateRequestToModel(requestBody);
            user.Shifts.Add(shift);
            await _repository.SaveChangesAsync();

            return Ok(ShiftMapper.CastModelToDto(shift));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding time off.");
        }
    }

    [Authorize]
    [HttpGet("GetUserShifts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUserShifts([FromQuery] string userId)
    {
        var output = await _repository.Shifts
            .Include(shift => shift.User)
            .Where(shift => shift.User != null && shift.User.Id == userId)
            .ToListAsync();

        if (output.Count == 0) return NotFound($"The user with id: {userId} had no shifts.");

        return Ok(output.Select(ShiftMapper.CastModelToDto).ToList());
    }

    [Authorize]
    [HttpPut("UpdateShift")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateShift([FromBody] ShiftDto request)
    {
        var shift = await _repository.Shifts.FindAsync(request.Id);
        if (shift == null) return NotFound("Entry could not be found.");

        ShiftMapper.UpdateModelFromDto(shift, request);

        await _repository.SaveChangesAsync();

        return Ok(ShiftMapper.CastModelToDto(shift));
    }

    [Authorize]
    [HttpDelete("DeleteShift")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteShift([FromQuery] string id)
    {
        try
        {
            var timeOff = await _repository.Shifts.FindAsync(id);
            if (timeOff == null) return NotFound("Entry could no be found.");

            _repository.Shifts.Remove(timeOff);
            await _repository.SaveChangesAsync();

            return Ok("Entry deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting entry.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting entry.");
        }
    }
}