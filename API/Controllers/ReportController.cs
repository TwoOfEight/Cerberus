﻿using API.Models.DTOs;
using API.Models.Entities;
using API.Models.Mappers;
using API.Persistence;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeOff = API.Models.Entities.TimeOff;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly Repository _repository;

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
public async Task<IActionResult> GetDailyUserReport([FromQuery] string userId, [FromQuery] int month,
    [FromQuery] int year)
{
    if (month < 1 || month > 12)
        return BadRequest("Invalid month. Please provide a value between 1 and 12.");

    var days = ReportService.GetDaysInMonth(month, year);

    // Fetch time-offs for the user in the specified month and year
    var timeOffs = await _repository.TimeOffs
        .Where(timeOff => timeOff.UserId == userId && timeOff.StartDate.Year == year)
        .ToListAsync();

    if (!timeOffs.Any())
        return NotFound($"The user with id: {userId} had no time offs in month {month}.");

    // Create a list of DailyReport objects
    var dailyReports = new List<DailyReport>();

    // Populate the reports
    foreach (var day in days)
    {
        var dailyReport = GetDailyReport(day, timeOffs);
        _logger.LogInformation($"Getting daily report: {dailyReport} for day {day}.");

        // Fetch shifts for the user on this day
        var shifts = await _repository.Shifts
            .Where(shift => shift.UserId == userId && shift.StartDate.Date == day.Date)
            .ToListAsync();

        // Add shifts to the daily report
        foreach (var shift in shifts)
        {
            if (dailyReport.WorkHours == default)
                dailyReport.WorkHours = 8; // Assuming a standard workday of 8 hours

            var timeOffHours = (int)(shift.StartDate - shift.EndDate).TotalHours;
            var workHours = 8 - timeOffHours;

            dailyReport.WorkHours += workHours;
            dailyReport.TimeOffHours += timeOffHours;
        }

        dailyReports.Add(dailyReport);
    }

    return Ok(dailyReports);
}

private DailyReport GetDailyReport(DateTime day, List<TimeOff> timeOffs)
{
    var dailyReport = new DailyReport
    {
        Date = day,
        TimeOffHours = 0,
        WorkHours = 8, // Assuming a standard workday of 8 hours
        TimeOffReason = string.Empty
    };

    foreach (var timeOff in timeOffs)
    {
        if (day >= timeOff.StartDate.Date && day <= timeOff.EndDate.Date)
        {
            var startOfDay = day.Date;
            var endOfDay = day.Date.AddDays(1).AddTicks(-1);

            var actualStart = timeOff.StartDate > startOfDay ? timeOff.StartDate : startOfDay;
            var actualEnd = timeOff.EndDate < endOfDay ? timeOff.EndDate : endOfDay;

            if (actualStart < actualEnd)
            {
                var timeOffHours = (actualEnd - actualStart).TotalHours;

                // Update the daily report
                dailyReport.TimeOffHours += (int)timeOffHours;
                dailyReport.WorkHours -= (int)timeOffHours;
                dailyReport.TimeOffReason = timeOff.Description;
            }
        }
    }

    return dailyReport;
}
}