using API.Models.Entities;
using API.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repository = API.Persistence.Repository;

namespace API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly Repository _appContext;

    public UserController(ILogger<UserController> logger, Repository appContext)
    {
        _logger = logger;
        _appContext = appContext;
    }

    [Authorize]
    [HttpPost("AddTimeOff")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddTimeOff([FromBody] TimeOffEntity timeOff, [FromQuery] string? userId)
    {
        try
        {
            if (userId == null) return BadRequest();
            var user = await _appContext.AppUsers.FindAsync(userId);
            if (user == null) return NotFound();
            user.TimeOffs.Add(timeOff);
            await _appContext.SaveChangesAsync();
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while adding time off.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding time off.");
        }
    }


    [Authorize]
    [HttpGet("GetTimeOff/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTimeOff(string id)
    {
        var timeOff = await _appContext.TimeOffs
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (timeOff == null)
        {
            return NotFound("Time off not found.");
        }

        return Ok(timeOff);
    }

    [Authorize]
    [HttpGet("{userId}/TimeOffs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUserTimeOffs(string userId)
    {
        var user = await _appContext.AppUsers
            .Include(u => u.TimeOffs)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return NotFound("User not found.");
        }

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
            var timeOff = await _appContext.TimeOffs.FindAsync(id);

            if (timeOff == null)
            {
                return NotFound("Time off not found.");
            }

            _appContext.TimeOffs.Remove(timeOff);
            await _appContext.SaveChangesAsync();

            return Ok("Time off deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting time off.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting time off.");
        }
    }
}
