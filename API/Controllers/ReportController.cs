using API.Models.DTOs;
using API.Models.Mappers;
using API.Persistence;
using API.Services;
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
    [HttpGet("GetUserDailyReport")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetDailyUserReport([FromQuery] string userId, [FromQuery] int month, [FromQuery] int year)
    {
        if (month < 1 || month > 12) return BadRequest("Invalid month. Please provide a value between 1 and 12.");

        List<DateTime> days = ReportService.GetDaysInMonth(month, year);

        // Fetch time-offs for the user in the specified month and year
        var timeOffs = await _repository.TimeOffs
            .Where(b => b.UserId == userId && b.StartDate.Year == year && b.StartDate.Month == month)
            .ToListAsync();

        if (!timeOffs.Any()) return NotFound($"The user with id: {userId} had no time offs in month {month}.");

        // Dictionary to hold daily time-off hours
        var dailyTimeOffHours = new Dictionary<DateTime, double>();

        // Initialize daily hours with 0
        foreach (var day in days)
        {
            dailyTimeOffHours[day] = 0;
        }

        // Calculate hours for each day
        foreach (var day in days)
        {
            foreach (var timeOff in timeOffs)
            {
                if (day >= timeOff.StartDate.Date && day <= timeOff.EndDate.Date)
                {
                    // Calculate hours for the day
                    var startOfDay = day.Date;
                    var endOfDay = day.Date.AddDays(1).AddTicks(-1);

                    // Determine actual start and end for the current day
                    var actualStart = timeOff.StartDate > startOfDay ? timeOff.StartDate : startOfDay;
                    var actualEnd = timeOff.EndDate < endOfDay ? timeOff.EndDate : endOfDay;

                    // Add the hours to the day's total
                    dailyTimeOffHours[day] += (actualEnd - actualStart).TotalHours;
                }
            }
        }

        // Format the response
        var result = dailyTimeOffHours
            .Select(kvp => new
            {
                Date = kvp.Key.ToString("yyyy-MM-dd"),
                TimeOffHours = kvp.Value
            })
            .ToList();

        return Ok(result);
    }
}