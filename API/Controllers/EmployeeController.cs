using API.Models.Entities;
using API.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : Controller
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly Repository _repository;

    public EmployeeController(ILogger<AuthenticationController> logger, Repository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [Authorize]
    [HttpPost("Create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] EmployeeEntity employee)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid employee data provided.");
                return BadRequest(ModelState); // 400 Bad Request
            }

            await _repository.Employees.AddAsync(employee);
            await _repository.SaveChangesAsync();

            _logger.LogInformation($"Employee {employee.Name} created successfully.");
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee); // 201 Created
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the employee.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    [Authorize]
    [HttpDelete("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        try
        {
            var employee = await _repository.Employees.FindAsync(id);

            if (employee == null)
            {
                _logger.LogWarning($"Employee with ID {id} not found.");
                return NotFound(); // 404 Not Found
            }

            _repository.Employees.Remove(employee);
            await _repository.SaveChangesAsync();

            _logger.LogInformation($"Employee with ID {id} deleted successfully.");
            return Ok(employee); // 200 OK
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the employee.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    [Authorize]
    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var employees = await _repository.Employees.ToListAsync();

            if (employees == null || employees.Count == 0)
            {
                _logger.LogWarning("No employees found in the database.");
                return NoContent(); // 204 No Content
            }

            _logger.LogInformation("Employees retrieved successfully.");
            return Ok(employees); // 200 OK
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching employees.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    [Authorize]
    [HttpGet("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromQuery] Guid id)
    {
        try
        {
            var employee = await _repository.Employees.FindAsync(id);

            if (employee == null)
            {
                _logger.LogWarning($"Employee with ID {id} not found.");
                return NotFound(); // 404 Not Found
            }

            _logger.LogInformation($"Employee with ID {id} retrieved successfully.");
            return Ok(employee); // 200 OK
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the employee.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    [Authorize]
    [HttpPut("Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] EmployeeEntity employee)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid employee data provided.");
                return BadRequest(ModelState); // 400 Bad Request
            }

            var existingEmployee = await _repository.Employees.FindAsync(employee.Id);
            if (existingEmployee == null)
            {
                _logger.LogWarning($"Employee with ID {employee.Id} not found.");
                return NotFound(); // 404 Not Found
            }

            _repository.Entry(existingEmployee).CurrentValues.SetValues(employee);
            await _repository.SaveChangesAsync();

            _logger.LogInformation($"Employee with ID {employee.Id} updated successfully.");
            return Ok(employee); // 200 OK
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the employee.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }
}